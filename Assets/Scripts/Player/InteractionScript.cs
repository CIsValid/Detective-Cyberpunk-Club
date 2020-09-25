using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [Header("Main Interaction Params")]
    [SerializeField]
    [Required]
    private Transform parentPickUp;
    public GameObject currentInteractedItem;
    private Rigidbody itemPickupRB;


    [Header("Interaction Settings")]
    public float sphereCastSize = 0.5f;
    public int indexForMask;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private PhysicsObject physicsObject;
    private Camera mainCamera;

    [Header("InteractionRotation")]
    public float rotationSpeed = 100f;
    Quaternion rotationLook;

    [Header("InteractionFollow")]
    [SerializeField]
    private float maxSpeed = 300f;
    [SerializeField]
    private float minSpeed = 0f;
    [SerializeField]
    private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(parentPickUp.position, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see what interactable object we are looking at
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if(Physics.SphereCast(raycastPos, sphereCastSize, mainCamera.transform.forward, out hit, maxDistance, 1 << indexForMask))
        {
            lookObject = hit.collider.transform.root.gameObject;
        }
        {
            lookObject = null;
        }

        // On button press Pick up or drop

        if(Input.GetButtonDown("Fire"))
        {
            if(currentInteractedItem == null)
            {
                if(lookObject != null)
                {
                    PickUpObject();
                }
            }
            else
            {
                BreakConnection();
            }
        }

        if(currentInteractedItem != null && currentDist > maxDistance)
        {
            BreakConnection();
        }

    }

    private void FixedUpdate() {
        if(currentInteractedItem != null)
        {
            currentDist = Vector3.Distance(parentPickUp.position, itemPickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = parentPickUp.position - itemPickupRB.position;
            itemPickupRB.velocity = direction.normalized * currentSpeed;
            //Rotation
            rotationLook = Quaternion.LookRotation(mainCamera.transform.position - itemPickupRB.position);
            rotationLook = Quaternion.Slerp(mainCamera.transform.rotation, rotationLook, rotationSpeed * Time.fixedDeltaTime);
            itemPickupRB.MoveRotation(rotationLook);
        }
    }

    public void PickUpObject()
    {
        physicsObject = lookObject.GetComponentInChildren<PhysicsObject>();
        currentInteractedItem = lookObject;
        itemPickupRB = currentInteractedItem.GetComponent<Rigidbody>();
        itemPickupRB.constraints = RigidbodyConstraints.FreezeRotation;
        physicsObject.interactionScript = this;
        StartCoroutine(physicsObject.PickUp());
    }

    public void BreakConnection()
    {
        itemPickupRB.constraints = RigidbodyConstraints.None;
        currentInteractedItem = null;
        physicsObject.pickedUp = false;
        currentDist = 0;
    }
}
