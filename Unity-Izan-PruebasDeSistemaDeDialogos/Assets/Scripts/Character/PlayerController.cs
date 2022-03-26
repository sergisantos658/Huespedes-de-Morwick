using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedRotation = 5.0f;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public static event Action<PlayerController> WhoIsPlayerController = delegate { };

    Rigidbody rb;

    private void OnEnable()
    {
        DialogueUI.StopDialogue += StopInteractingDialogue;
    }

    private void OnDisable()
    {
        DialogueUI.StopDialogue -= StopInteractingDialogue;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        WhoIsPlayerController(this);
    }


    void Update()
    {
        if (dialogueUI.isOpen) { rb.velocity = Vector3.zero; return; }
        Move();
        LookAtTheMouse();

    }

    void Move()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = dir * speed;
    }

    void LookAtTheMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist;

        if(playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    void StopInteractingDialogue()
    {
        GetComponent<InteractDialogueOrObjects>().StopInteract();
    }

    void IamPlayerController(PlayerController transferPlayerController)
    {
        Debug.Log("1 " + transferPlayerController);
        transferPlayerController = this;
        Debug.Log("2 " + transferPlayerController);
    }
}
