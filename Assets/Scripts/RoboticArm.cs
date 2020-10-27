using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArm : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject currentItemObject;
    private Outline outline;
    private bool timeReached;
    [SerializeField] private float timeSpeed;
    private Renderer rend;
    [SerializeField] private bool hasBeenHighligheted;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float blinkSpeed;

    private PlayerManager playerManager;

    [SerializeField] private AudioSource dialogueSource;
    [SerializeField] private AudioClip roboArmClip;

    [SerializeField] private bool hasBeenFound;

    private void Start()
    {
        currentItemObject = this.gameObject;

        rend = currentItemObject.GetComponent<Renderer>();
        
        outline = currentItemObject.GetComponent<Outline>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasBeenFound)
            {
                if (!target)
                {
                    target = other.gameObject;
                }

                if (!playerManager && target)
                {
                    playerManager = target.GetComponent<PlayerManager>();
                }

                if (playerManager && target && !hasBeenFound)
                {
                    hasBeenHighligheted = false;

                    dialogueSource.clip = roboArmClip;
                
                    dialogueSource.Play();

                    hasBeenFound = true;

                }
            }
        }
    }

    private void Update()
    {
        
        if(rend.isVisible && (target.transform.position - currentItemObject.transform.position).sqrMagnitude < detectionDistance * detectionDistance && hasBeenHighligheted)
        {
            HighlightItem();
        }
        else
        {
            HighlightDisable();
        }
        
        if(outline.enabled)
        {
            if (!timeReached)
            {
                timeSpeed += Time.deltaTime;
                
                if (timeSpeed >= 4)
                {
                    timeReached = true;
                }
            }
            else
            {
                timeSpeed -= Time.deltaTime;

                if (timeSpeed <= 0)
                {
                    timeReached = false;
                }
                
            }

            outline.OutlineWidth = Mathf.Lerp(outline.OutlineWidth, timeSpeed, Time.deltaTime * blinkSpeed);

        }
    }
    
    private void HighlightItem()
    {
        // Highlight the item so the player knows theres an interactable there
        outline.enabled = true;
    }

    private void HighlightDisable()
    {
        outline.enabled = false;
    }
}
