using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyskasterColider : MonoBehaviour
{
    public GameObject lyskasterOBJ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lyskaster"))
        {
            
            if (lyskasterOBJ.GetComponent<Lyskaster>().direction == -1)
            {
                lyskasterOBJ.GetComponent<Lyskaster>().direction = 1;
            }
            
            else if (lyskasterOBJ.GetComponent<Lyskaster>().direction == 1)
            {
                lyskasterOBJ.GetComponent<Lyskaster>().direction = -1;
            }
        }
    }
}
