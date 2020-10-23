using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class Footsteps : MonoBehaviour
{
    public AudioClip[] audioClips;
    private CharacterController characterController;
    private AudioSource audioSource;
    private PlayerController playerController;
    private int stepCount = 0;
    private float initSpeed;
    public float walkSpeed = 0.4f;
    public float runSpeed = 0.3f;

    private bool exhausted;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        initSpeed -= Time.deltaTime;
        
        if (initSpeed <= 0)
        {
            exhausted = false;
            
            if (playerController.audioRun)
            {
                initSpeed = runSpeed;
            }
            else
            {
                initSpeed = walkSpeed;
            }
            
        }
        else
        {
            exhausted = true;
        }
        
        if (characterController.isGrounded && characterController.velocity.magnitude > 1f &&
            audioSource.isPlaying == false && !exhausted)
        {
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.PlayOneShot(audioClips[stepCount]);
            stepCount++;
        }
        
        switch (stepCount)
        {
            case 0:
                audioSource.clip = audioClips[0];
                break;
            case 1:
                audioSource.clip = audioClips[1];
                break;
            case 2:
                audioSource.clip = audioClips[0];
                stepCount = 0;
                break;
        }
    }
}
