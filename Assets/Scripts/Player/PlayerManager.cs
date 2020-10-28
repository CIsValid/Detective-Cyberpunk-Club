using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AudioSource dialogueSource;
    [SerializeField] private AudioClip talkAboutVip;
    [SerializeField] private AudioClip talkAboutDevice;

    private GameObject keyCard;
    private GameObject elecButton;
    private GameObject roboArm;

    public bool hasPressedButton;
    public bool hasKeyCard;

    public bool hasKeyCardCallable;
    public bool hasButtonCallable;
    
    //private Completed completedkey;
    private InspectObjectScript completedkey;
    //private Completed completedelecbutton;
    private InspectObjectScript completedelecbutton;
    private bool hasTalkedAboutKey;
    private bool hasTalkedAboutButton;
    private Completed completedkeyConfirm;
    private Completed completedelecbuttonConfirm;

    private void Start()
    {
        // find key with tag
        keyCard = GameObject.FindGameObjectWithTag("keycard");
        completedkeyConfirm = keyCard.GetComponent<Completed>();
        completedkey = keyCard.GetComponent<InspectObjectScript>();
        // find button with tag
        elecButton = GameObject.FindGameObjectWithTag("elecbutton");
        completedelecbuttonConfirm = elecButton.GetComponent<Completed>();
        completedelecbutton = elecButton.GetComponent<InspectObjectScript>();
    }

    private void Update()
    {
        // set bool to true or false depending on their interaction status here
        if (keyCard)
        {
            //hasKeyCard = completedkey.completed;
            hasKeyCard = completedkey.hasPressedToInteract;
            hasKeyCardCallable = completedkeyConfirm.completed;


            if (!hasTalkedAboutKey && hasKeyCard)
            {
                dialogueSource.clip = talkAboutVip;
                
                dialogueSource.Play();

                hasTalkedAboutKey = true;
            }
            
        }
        
        if (elecButton && keyCard)
        {
            hasPressedButton = completedelecbutton.hasPressedToInteract;
            hasButtonCallable = completedelecbuttonConfirm.completed;

            if (!hasTalkedAboutButton && hasPressedButton)
            {
                dialogueSource.clip = talkAboutDevice;
                
                dialogueSource.Play();

                hasTalkedAboutButton = true;
            }
        }

    }

}
