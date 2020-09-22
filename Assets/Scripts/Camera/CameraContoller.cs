using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [Header("Camera Target")]
    [Required]
    public GameObject cameraTarget;
    private Transform cameraPos;
    private Vector3 targetCameraBobPos;


    [Header("Camera Breathing Offsets")]
    [Range(0f, 0.5f)]
    public float cameraUpDownOffsetSlider;
    [Range(0, 0.5f)]
    public float cameraSideOffsetSlider;


    [Header("Camera Breathing Frequency")]
    public float breathingFrequency;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the private variable to the Camera Game Component
        cameraPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Breathing
        HeadBob(breathingFrequency, cameraSideOffsetSlider, cameraUpDownOffsetSlider);
        breathingFrequency += Time.deltaTime;
    }

    void HeadBob(float pos_z, float pos_x_intensity, float pos_y_intensity)
    {
        cameraPos.localPosition = cameraPos.position + new Vector3 (Mathf.Cos(pos_z) * pos_x_intensity, Mathf.Sin(pos_z) * pos_y_intensity, 0);
    }
}
