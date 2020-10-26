using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject keyCard;
    private GameObject elecButton;

    public bool hasPressedButton;
    public bool hasKeyCard;
    private Completed completedkey;
    private Completed inspectObjectScriptbutton;

    private void Start()
    {
        // find key with tag
        keyCard = GameObject.FindGameObjectWithTag("keycard");
        completedkey = keyCard.GetComponent<Completed>();
        // find button with tag
        //elecButton = GameObject.FindGameObjectWithTag("elecbutton");
        //inspectObjectScriptbutton = elecButton.GetComponent<InspectObjectScript>();


    }

    private void Update()
    {
        // set bool to true or false depending on their interaction status here
        if (keyCard)
        {
            hasKeyCard = completedkey.completed;
        }
        
        /*if (elecButton)
        {
            hasPressedButton = inspectObjectScriptbutton.completed;
        }*/
        
    }
    
    
}
