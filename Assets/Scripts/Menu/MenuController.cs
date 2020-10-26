using System.Timers;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject door;
    public AudioClip voicelinesstart;

    public GameObject currentPanel;
    public GameObject exitPromptPanel;

    public Vector3 newPlayerPos;

    public float doorTimer = 1f;
    public float playerTimer = 1f;
    public float timeBeforeWalk = 1f;

    private bool pressedPlay;
    private bool playerInside;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        player.GetComponent<PlayerController>().enabled = false;
        camera.GetComponent<CameraContoller>().enabled = false;
        
    }

    private void Update()
    {
        if (pressedPlay)
        {
            if (!playerInside)
            {
                doorTimer -= Time.deltaTime;

                {
                    if (doorTimer <= 0)
                    {
                        door.GetComponent<Animator>().SetBool("timerDone", true);

                        timeBeforeWalk -= Time.deltaTime;

                        playerTimer -= Time.deltaTime;

                        if (timeBeforeWalk <= 0)
                        {
                            player.transform.position = Vector3.Lerp(player.transform.position, newPlayerPos, 0.008f);

                        }

                        if (playerTimer <= 0)
                        {
                            playerInside = true;
                            introComplete();
                        }
                    }
                }
            }
            else
            {
                door.GetComponent<Animator>().SetBool("playerInside", true);

                playerTimer = 4f;
                doorTimer = 4f;
                timeBeforeWalk = 4f;
            }
 
        }
        else
        {
            door.GetComponent<Animator>().SetBool("playerInside", false);
            
        }

    }

    public void StartGame()
    {
        player.GetComponent<AudioSource>().clip = voicelinesstart;
        currentPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pressedPlay = true;

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
    }
}
