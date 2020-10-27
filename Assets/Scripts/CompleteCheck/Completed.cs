using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completed : MonoBehaviour
{
    public bool completed;
    
    public List<GameObject> objectsToDelete = new List<GameObject>();

    private void Update()
    {
        if (completed)
        {
            Destroy(this.GetComponent<MeshFilter>());
            Destroy(this.GetComponent<MeshRenderer>());
            Destroy(this.GetComponent<InspectObjectScript>());
            Destroy(this.GetComponent<Outline>());

            if (objectsToDelete.Count >= 1)
            {
                for (int i = 0; i < objectsToDelete.Count; i++)
                {
                    Destroy(objectsToDelete[i]);
                    objectsToDelete.Remove(objectsToDelete[i]);

                }
            }
        }
    }
}
