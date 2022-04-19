using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(InteractDialogueOrObjects))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float speed = 5.0f;
    //[SerializeField] private float speedRotation = 5.0f;

    private static GameObject _moveToIndicatorInstance;

    [SerializeField]
    private float _feedbackPointerScale = 1f; // Scale the pointer

    [SerializeField]
    private GameObject _moveToIndicator; // Prefab of the pointer 3D

    [SerializeField]
    private GameObject mainCamera; // Main camera

    [SerializeField] 
    private DialogueUI dialogueUI;

    [SerializeField] 
    private LayerMask layerMask;

    public DialogueUI DialogueUI => dialogueUI;

    public static event Action<PlayerController> WhoIsPlayerController = delegate { };

    [SerializeField]
    private float angleRotate;

    [SerializeField]
    private float distance;

    bool onlyOnce = false;
    Vector3 sampleVector;
    Rigidbody rb;
    NavMeshAgent agent;
    Camera m_Camera;

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

        agent = GetComponent<NavMeshAgent>();

        m_Camera = mainCamera.GetComponent<Camera>();

        WhoIsPlayerController(this);

        PreparePointer(_moveToIndicator, _feedbackPointerScale);
    }


    void Update()
    {
        Vector3 tempV = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(sampleVector.x, 0, sampleVector.z);
        float tempOffset = tempV.magnitude;

        if ( tempOffset <= 2f && onlyOnce == true)
        {
            agent.isStopped = true;
            transform.position = transform.position;

            onlyOnce = false;
        }

        if (dialogueUI.isOpen) {
            /*rb.velocity = Vector3.zero;*/ 
            agent.isStopped = true; 

            return; 
        }
        Move();

    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.nearClipPlane));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 100, -1);

                if (!navHit.hit)
                    return;

                var path = new NavMeshPath();
                if (!agent.CalculatePath(navHit.position, path) || path.status == NavMeshPathStatus.PathInvalid)
                    return;

                ShowPointer(navHit.position);
                sampleVector = navHit.position;

                if (agent.isStopped) agent.isStopped = false;

                onlyOnce = true;

                agent.SetPath(path);
            }


        }

        //if(angleRotate > 5.0f)
        //{

        //}
        //else
        //{
        //    if(distance > 0.1f)
        //    {
        //        agent.Move(ray.position);
        //    }
        //    else
        //    {
        //        agent.isStopped = true;
        //    }
        //}

        //var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //rb.velocity = dir * speed;
    }

    void StopInteractingDialogue()
    {
        GetComponent<InteractDialogueOrObjects>().StopInteract();
    }



    public void PreparePointer(GameObject prefabInstance, float scale)
    {
        if (prefabInstance == null) return;

        if (_moveToIndicatorInstance == null)
            _moveToIndicatorInstance = GameObject.Instantiate(prefabInstance);

        _moveToIndicatorInstance.SetActive(false);
        _moveToIndicatorInstance.transform.localScale = Vector3.one * scale;

        GameObject.DontDestroyOnLoad(_moveToIndicatorInstance);
    }

    public void ShowPointer(Vector3 position)
    {
        _moveToIndicatorInstance?.SetActive(false);
        if (_moveToIndicatorInstance != null) _moveToIndicatorInstance.transform.position = position;
        _moveToIndicatorInstance?.SetActive(true);
    }

}
