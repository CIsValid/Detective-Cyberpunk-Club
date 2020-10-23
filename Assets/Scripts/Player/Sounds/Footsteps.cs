using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class Footsteps : MonoBehaviour
{
    public AudioClip[] audioClips;
    private CharacterController characterController;
    private AudioSource audioSource;
    private int stepCount;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 1f &&
            audioSource.isPlaying == false)
        {
            audioSource.Play();
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
