using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectObjectScript : MonoBehaviour
{
    // Public Variables

    [Header("Text Object")]
    [Required]
    public Text text;

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

    // Camera
    private Camera mainCamera;

    // Renderer
    private MeshRenderer m_Renderer;

    // Bool

    private bool hasPressedToInteract;

    private void Start() {
        m_Renderer = this.gameObject.GetComponent<MeshRenderer>();
        mainCamera = Camera.main;
        currentObjectPos = this.transform.localPosition;
        oldPosition = this.transform.localPosition;
        oldRotation = this.transform.localRotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!hasPressedToInteract && m_Renderer.isVisible) text.text = interactionMessage;
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

    private void StartInspection()
    {

    }
    private void ExitInspection()
    {

    }

}
