using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicePowerOn : MonoBehaviour
{
    public GameObject poleParticleSystem;
    public GameObject poleParticleSystem2;
    public AudioSource audioSource;

    private GameObject target;
    private PlayerManager playerManager;

    private bool hasPlayedSound;

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
                // Check if he has interacted with button

                if (playerManager.hasPressedButton)
                {
                    
                    // turn on the vfx
                    poleParticleSystem.SetActive(true);
                    poleParticleSystem2.SetActive(true);
                    
                    // Play sound

                    if (!hasPlayedSound)
                    {
                        audioSource.Play();
                        hasPlayedSound = true;
                    }
                }
            }
        }
    }
}
