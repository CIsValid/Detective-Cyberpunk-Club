﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AudioSource dialogueSource;
    [SerializeField] private AudioClip doorOpeningCall;
    [SerializeField] private AudioClip talkAboutVip;
    [SerializeField] private AudioClip talkAboutDevice;
    [SerializeField] private AudioClip talkAboutArm;
    
    private GameObject keyCard;
    private GameObject elecButton;
    private GameObject roboArm;

    public bool hasPressedButton;
    public bool hasKeyCard;
    
    private Completed completedkey;
    private Completed completedelecbutton;
    private bool hasTalkedAboutKey;
    private bool hasTalkedAboutButton;
    private bool hasHadFirstPhoneCall;

    private void Start()
    {
        // find key with tag
        keyCard = GameObject.FindGameObjectWithTag("keycard");
        completedkey = keyCard.GetComponent<Completed>();
        // find button with tag
        elecButton = GameObject.FindGameObjectWithTag("elecbutton");
        completedelecbutton = elecButton.GetComponent<Completed>();
        
        // Play First Voice File
        if (!hasHadFirstPhoneCall)
        {
            dialogueSource.clip = doorOpeningCall;
        
            dialogueSource.Play();
        }


    }

    private void Update()
    {
        // set bool to true or false depending on their interaction status here
        if (keyCard)
        {
            hasKeyCard = completedkey.completed;

            if (!hasTalkedAboutKey)
            {
                dialogueSource.clip = talkAboutVip;
                
                dialogueSource.Play();

                hasTalkedAboutKey = true;
            }
            
        }
        
        if (elecButton && keyCard)
        {
            hasPressedButton = completedelecbutton.completed;

            if (!hasTalkedAboutButton)
            {
                dialogueSource.clip = talkAboutDevice;
                
                dialogueSource.Play();

                hasTalkedAboutButton = true;
            }
        }

    }
    
    
}
