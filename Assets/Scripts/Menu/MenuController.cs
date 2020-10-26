using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject door;
    public AudioClip voicelinesstart;

    public GameObject currentPanel;
    public GameObject exitPromptPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        player.GetComponent<PlayerController>().enabled = false;
        camera.GetComponent<CameraContoller>().enabled = false;
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        player.GetComponent<AudioSource>().clip = voicelinesstart;
        currentPanel.SetActive(false);

    }

    public void ExitGame()
    {
        exitPromptPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
    
    public void ExitGameYes()
    {
        Application.Quit();
    }
    
    public void ExitGameNo()
    {
        exitPromptPanel.SetActive(false);
        currentPanel.SetActive(true);
    }

    public void introComplete()
    {
        player.GetComponent<PlayerController>().enabled = true;
        camera.GetComponent<CameraContoller>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
