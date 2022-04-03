using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float speed = 5.0f;
    //[SerializeField] private float speedRotation = 5.0f;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private LayerMask layerMask;

    public DialogueUI DialogueUI => dialogueUI;

    public static event Action<PlayerController> WhoIsPlayerController = delegate { };

    Rigidbody rb;
    NavMeshAgent agent;

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
        //rb = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        
        WhoIsPlayerController(this);
    }


    void Update()
    {
        if (dialogueUI.isOpen) { /*rb.velocity = Vector3.zero;*/ return; }
        Move();

    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(agent);
                agent.SetDestination(hit.point);
            }

        }

        //var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //rb.velocity = dir * speed;
    }

    void StopInteractingDialogue()
    {
        GetComponent<InteractDialogueOrObjects>().StopInteract();
    }

}
