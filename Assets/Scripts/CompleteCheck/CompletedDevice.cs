using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedDevice : MonoBehaviour
{
    public bool completed;

    private void Update()
    {
        if (completed)
        {
            Debug.Log("cool");
        }
    }
}
