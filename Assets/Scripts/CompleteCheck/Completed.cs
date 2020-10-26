using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completed : MonoBehaviour
{
    public bool completed;

    private void Update()
    {
        if (completed)
        {
            Destroy(this.GetComponent<MeshFilter>());
            Destroy(this.GetComponent<MeshRenderer>());
            Destroy(this.GetComponent<InspectObjectScript>());
            Destroy(this.GetComponent<Outline>());
        }
    }
}
