using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectObjectScript : MonoBehaviour
{
    // Public Variables
    
    [Header("Player & Target")]
    [Required]
    public GameObject player;

    private GameObject currentItemObject;

    [Header("Text Object")]
    [Required]
    public Text text;

    [Header("Distance To Start Highlight")]
    public float detectionDistance = 20f;

    [Header("Item Info")]
    public string itemName;
    public string itemDescripion;
    public string interactionMessage;

    // Private Variables
    // Object Position and Rotation
    private Vector3 currentObjectPos;
    private Quaternion currentObjectRot;
    private Vector3 oldPosition;
    private Quaternion oldRotation;

    private CameraContoller cameraContoller;
    private PlayerController playerController;

    // Camera
    private Camera mainCamera;

    // Renderer
    private MeshRenderer m_Renderer;

    // Bool

    private bool hasPressedToInteract;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        currentItemObject = this.gameObject;
        
        m_Renderer = this.gameObject.GetComponent<MeshRenderer>();
        mainCamera = Camera.main;
        cameraContoller = mainCamera.GetComponent<CameraContoller>();
        currentObjectPos = this.transform.localPosition;
        oldPosition = this.transform.localPosition;
        oldRotation = this.transform.localRotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!hasPressedToInteract && m_Renderer.isVisible)text.text = interactionMessage;
            else
            {
                text.text = null;
            }

            if(Input.GetKey(KeyCode.E))
            {
                hasPressedToInteract = true;
                StartInspection();
                text.text = null;
            }

            if (Input.GetKey(KeyCode.Escape) && hasPressedToInteract)
            {
                ExitInspection();
            }
        }
    }

    private void Update()
    {
        if (m_Renderer.isVisible && (player.transform.position - currentObjectPos).sqrMagnitude < detectionDistance * detectionDistance)
        {
            HighlightItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hasPressedToInteract = false;
            text.text = null;
        }
    }

    private void HighlightItem()
    {
        // Highlight the item so the player knows theres an interactable there
        Debug.Log("hi");
    }

    private void StartInspection()
    {
        // Blur Background except for Item being Inspected
        // Move Object to front of camera
        // Scale Object to camera? 
        // Activate Inspection Controller
        InspectionController();
        // Deactivate Camera Controller & Player controller
        // Play Audio file
    }
    private void ExitInspection()
    {
        // Move object back into original position
        // Change Scale to original Scale
        // remove the blur effect
        // Blur the background
        // Deactivate Inspection Controller
        // Activate Camera controller + Player controller
    }

    private void InspectionController()
    {
        // Make camera rotate around the object with mouse input
        // Make W & S Zoom in & Zoom out
    }

}
