using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class PlayerInteraction : MonoBehaviour
{
    [Header("Ray settings"), Space(5)]
    #region Data&Settings

    public float rayDistance; // Set the distance from the ray 
    public float raysphereRadius; // Set the Bounds of the interaction Raycast
    public LayerMask interactableLayer; // Set the layerMask to only interact with
    [SerializeField] private GameObject mainCamera;
    Camera mainCameraC;

    float holdTime; // the time updating until the holdTime interactable complete

    float timeToUseAgain = 0f; // Time to interact again after interact with an object
    bool iInteract = false; // If i interact with an object before or not

    bool onlyOnce = false;
    bool onlyTwice = false;

    // Info to use mouse on Camera
    Vector3 mpS;
    Vector3 mpW;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    float distancePlayer;

    Interactable interactable;

    void IncreaseHoldTime() => holdTime += Time.deltaTime;
    void ResetHoldTime() => holdTime = 0f;

    #endregion

    [Header("CursorSettings"), Space(5)]
    #region Cursor

    [SerializeField] private Texture2D interactCursor;
    [SerializeField] private Texture2D normalCursor;

    private Vector2 interactCursorHotspot;
    private Vector2 normalCursorHotspot;
    #endregion

    [Space(20)]
    public GameObject interactionHoldGO; // the ui parent to disable when not interacting
    public UnityEngine.UI.Image interactionHoldProgress; // the progress bar for hold interaction type


    void Start()
    {
        mainCameraC = mainCamera.GetComponent<Camera>();


        CursorSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerController>()) { if (gameObject.GetComponent<PlayerController>().DialogueUI.isOpen) { return; } }

        mpS = Input.mousePosition;
        mpS = new Vector3(mpS.x, mpS.y, mainCameraC.nearClipPlane);

        mpW = mainCameraC.ScreenToWorldPoint(mpS);

        rayOrigin = mainCameraC.transform.position;
        rayDirection = (mpW - mainCameraC.transform.position);

        hitAndInteraction();
    }

    void CursorSettings()
    {
        interactCursorHotspot = new Vector2(interactCursor.width / 2 - 2, interactCursor.height / 2 - 6);
        normalCursorHotspot = new Vector2(normalCursor.width / 2 - 2, normalCursor.height / 2 - 6);
        Cursor.SetCursor(normalCursor, normalCursorHotspot, CursorMode.ForceSoftware);
    }

    void hitAndInteraction()
    {
        Ray ray = new Ray(rayOrigin, rayDirection);

        RaycastHit hitInfo;

        bool hitSomething = Physics.SphereCast(ray, raysphereRadius, out hitInfo, Mathf.Infinity, interactableLayer);;

        bool interactMouseButton = Input.GetMouseButtonDown(0) ? true : false;

        if (hitSomething)
        {
            if (!onlyOnce)
            {
                Cursor.SetCursor(interactCursor, interactCursorHotspot, CursorMode.ForceSoftware);
                onlyOnce = true;
            }

            if (interactMouseButton || Input.GetKeyDown(KeyCode.Mouse1))
            {
                interactable = hitInfo.collider.GetComponent<Interactable>();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) // Observation
            {
                interactable?.Observation();
                interactable = null;
            }

        }
        else
        {

            if (interactMouseButton)
            {
                interactable = null;
                gameObject.GetComponent<InteractDialogueOrObjects>().StopInteract();
                ResetHoldTime();

            }

            if (onlyOnce)
            {
                Cursor.SetCursor(normalCursor, normalCursorHotspot, CursorMode.ForceSoftware);
                gameObject.GetComponent<InteractDialogueOrObjects>().StopInteract();
                onlyOnce = false;
            }
        }

        if (interactable != null && !iInteract)
        {
            distancePlayer = Vector3.Distance(interactable.GetComponent<Collider>().ClosestPoint(gameObject.transform.position), gameObject.transform.position);

            bool closeEnought = distancePlayer < rayDistance;

            interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);

            if (closeEnought)
            {
                gameObject.GetComponent<InteractDialogueOrObjects>().Interacting();
                HandleInteraction(interactable);
                onlyTwice = true;
            }
            else
            {
                if (onlyTwice)
                {
                    gameObject.GetComponent<InteractDialogueOrObjects>().StopInteract();
                    ResetHoldTime();
                    onlyTwice = false;
                }
                
            }

        }
        else
        {
            gameObject.GetComponent<InteractDialogueOrObjects>().StopInteract();
        }

        if (timeToUseAgain <= 0)
            iInteract = false;
        else
            timeToUseAgain -= Time.deltaTime;

        // Line from Player To HitPoint
        Debug.DrawLine(gameObject.transform.position, hitInfo.point, Color.green);

        // Line from Camera To World
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * (hitSomething ? hitInfo.distance : 100), hitSomething ? Color.green : Color.red);
    }

    void HandleInteraction(Interactable interactable)
    {
        //KeyCode key = KeyCode.Mouse0;

        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact
                
                interactable.Interact();
                    
                timeToUseAgain = 1.3f;
                iInteract = true;

                this.interactable = null;


                break;
            case Interactable.InteractionType.Hold:
                
                // we are holding the key, increase the timer until we reach the max
                IncreaseHoldTime();
                if (holdTime >= interactable.GetHoldTime())
                {
                    interactable.Interact();
                    ResetHoldTime();

                    timeToUseAgain = 1.3f; // The time until interact with an object again
                    iInteract = true;

                    this.interactable = null;
                }

                interactionHoldProgress.fillAmount = holdTime / interactable.GetHoldTime();
                break;
            // here is started code for your custom interaction :)
            case Interactable.InteractionType.Minigame:
                // here you make ur minigame appear
                break;

            // helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

}
