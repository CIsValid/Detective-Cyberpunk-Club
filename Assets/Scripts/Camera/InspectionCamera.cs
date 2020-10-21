using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCamera : MonoBehaviour
{
    private Camera inspectionCamera;
    private Camera mainCamera;

    public GameObject objectToInpect;

    public bool inspectionState;

    private void Start()
    {
        inspectionCamera = this.gameObject.GetComponent<Camera>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        
        if (!inspectionState && objectToInpect == null)
        {
            inspectionCamera.transform.position = mainCamera.transform.position;
            inspectionCamera.transform.rotation = mainCamera.transform.rotation;
        }
        else
        {
            if (objectToInpect != null)
            {
                inspectionCamera.transform.LookAt(objectToInpect.transform);
            }
            
            inspectionCamera.transform.position = mainCamera.transform.position;
            inspectionCamera.transform.rotation = mainCamera.transform.rotation;
        }
    }
}
