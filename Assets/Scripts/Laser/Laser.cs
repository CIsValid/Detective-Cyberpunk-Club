using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject Lyskaster;
    private LineRenderer lr;
    private Vector3 LyskasterPosition;
    private Quaternion LyskasterRotation;

    // Start is called before the first frame update
    void Start() {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        
        LyskasterPosition = Lyskaster.transform.position;
        LyskasterRotation = Lyskaster.transform.rotation;
        lr.SetPosition(0, LyskasterPosition);
        lr.transform.rotation = LyskasterRotation;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1,  transform.forward*5000);
    }
}
