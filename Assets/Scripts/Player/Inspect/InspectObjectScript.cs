using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectObjectScript : MonoBehaviour
{
    // Public Variables
    
    [Header("Player & Target & Main Camera")]
    [Required]
    public GameObject player;
    private Camera mainCamera;

    private GameObject currentItemObject;

    [Header("Text Object")]
    [Required]
    public Text text;

    [Header("Distance To Start Highlight")]
    public float detectionDistance = 20f;
    
    [Header("Inspection Camera Offset")]
    public float cameraOffset = 0f;

    [Header("Object Scale")]
    public Vector3 InspectionScale;
    private Vector3 oldScale;
    
    [Header("Object Rotation")]
    public Quaternion InspectionRotation = Quaternion.Euler(1f,0f,0f);
    
    [Header("Blink Speed")]
    public float blinkSpeed = 50f;
    private float transitionSpeed = 0.5f;

    [Header("Item Info")]
    public string itemName;
    [Multiline]
    public string itemDescripion;
    public string interactionMessage;

    // Private Variables
    // Object Position and Rotation
    private Vector3 oldObjectPos;
    private Quaternion oldObjectRot;

    private CameraContoller cameraContoller;
    private PlayerController playerController;

    private Outline outline;
    
    // Renderer
    private MeshRenderer m_Renderer;

    // Bool

    public bool hasPressedToInteract;
    private bool hasBeenHighlighted;

    public float timeSpeed = 0;

    private bool timeReached;

    private bool canInteract;

    public bool completed;

    public bool destroyAfter;

    private void Start()
    {
        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        currentItemObject = this.gameObject;
        
        m_Renderer = this.gameObject.GetComponent<MeshRenderer>();
        cameraContoller = mainCamera.GetComponent<CameraContoller>();
        oldObjectPos = currentItemObject.transform.localPosition;
        oldObjectRot = currentItemObject.transform.localRotation;

        outline = currentItemObject.GetComponent<Outline>();
        
        transitionSpeed = Time.deltaTime * transitionSpeed;

        oldScale = currentItemObject.transform.localScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!hasPressedToInteract && m_Renderer.isVisible && !completed)
            {
                text.text = interactionMessage;
                canInteract = true;
            }
            else
            {
                text.text = null;
            }
        }
    }

    private void Update()
    {
        if (canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                hasPressedToInteract = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && hasPressedToInteract)
            {
                if (!destroyAfter && !completed)
                {
                    hasPressedToInteract = false;
                }
                else
                {
                    completed = true;
                    playerController.enabled = true;
                    cameraContoller.enabled = true;
                    hasPressedToInteract = false;
                    
                    this.GetComponent<Completed>().completed = true;
                }
            }
        }

        if (hasPressedToInteract)
        {
            hasBeenHighlighted = true;
            StartInspection();
            text.text = null;

        }
        if (!hasPressedToInteract)
        {
            ExitInspection();
        }

        if (m_Renderer.isVisible &&
            (player.transform.position - oldObjectPos).sqrMagnitude < detectionDistance * detectionDistance &&
            !hasBeenHighlighted)
        {
            HighlightItem();
        }
        else
        {
            HighlightDisable();
        }

        if (outline.enabled)
        {
            if (!timeReached)
            {
                timeSpeed += Time.deltaTime;
                
                if (timeSpeed >= 4)
                {
                    timeReached = true;
                }
            }
            else
            {
                timeSpeed -= Time.deltaTime;

                if (timeSpeed <= 0)
                {
                    timeReached = false;
                }
                
            }

            outline.OutlineWidth = Mathf.Lerp(outline.OutlineWidth, timeSpeed, Time.deltaTime * blinkSpeed);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.text = null;
            if (!hasPressedToInteract)
            {
                canInteract = false;
            }
        }
    }

    private void HighlightItem()
    {
        // Highlight the item so the player knows theres an interactable there
        outline.enabled = true;
    }

    private void HighlightDisable()
    {
        outline.enabled = false;
    }

    private void StartInspection()
    {
        // Blur Background except for Item being Inspected
        // Move Object to front of camera
        currentItemObject.transform.position = Vector3.Lerp(currentItemObject.transform.position,
            mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraOffset)), transitionSpeed);
        // Rotate Object Towards Camera
        if(currentItemObject.transform.rotation != mainCamera.transform.rotation)
        currentItemObject.transform.rotation = Quaternion.Lerp(currentItemObject.transform.rotation, mainCamera.transform.rotation * InspectionRotation, transitionSpeed);
        // Scale Object to camera? 
        currentItemObject.transform.localScale =
            Vector3.Lerp(currentItemObject.transform.localScale, InspectionScale, transitionSpeed);
        // Play Audio file
        // Activate Inspection Controller
        InspectionController();
    }
    private void ExitInspection()
    {
        // Move object back into original position
        currentItemObject.transform.position = Vector3.Lerp(currentItemObject.transform.position,
            oldObjectPos, transitionSpeed);
        // Change Rotation the the original
        if(currentItemObject.transform.rotation != oldObjectRot)
            currentItemObject.transform.rotation = Quaternion.Lerp(currentItemObject.transform.rotation, oldObjectRot, transitionSpeed);
        // Change Scale to original Scale
        currentItemObject.transform.localScale =
            Vector3.Lerp(currentItemObject.transform.localScale, oldScale, transitionSpeed);
        // remove the blur effect
        // Activate Camera controller + Player controller & Deactivate Inspection Controller
        if(Input.GetKeyDown(KeyCode.Escape) && hasPressedToInteract) ExitInspectionController();
    }

    private void InspectionController()
    {
        // Deactivate Camera Controller & Player controller
        playerController.enabled = false;
        cameraContoller.enabled = false;
        // Set interaction to Complete
        // Make object rotate around with mouse input & Make W & S Zoom in & Zoom out

    }

    private void ExitInspectionController()
    {
        completed = true;
        playerController.enabled = true;
        cameraContoller.enabled = true;
        hasPressedToInteract = false;

    }
}
