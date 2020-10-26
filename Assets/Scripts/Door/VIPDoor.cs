using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIPDoor : MonoBehaviour
{
    public GameObject doorToAffect;
    public Material newMaterial;

    private GameObject target;
    private PlayerManager playerManager;
    private Renderer rend;
    private AudioSource audioSource;

    private void Start()
    {
        if (!audioSource)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }
        
        rend = doorToAffect.GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!target)
            {
                target = other.gameObject;
            }

            if (!playerManager && target)
            {
                playerManager = target.GetComponent<PlayerManager>();
            }

            if (playerManager && target)
            {
                // Check if he has interacted with keycard

                if (playerManager.hasKeyCard)
                {
                    // change material on door

                    rend.material = newMaterial;
                
                    // Disable Collider
                    doorToAffect.GetComponent<BoxCollider>().enabled = false;
                    
                    // Play sound
                    audioSource.Play();
                }
            }
        }
    }
}
