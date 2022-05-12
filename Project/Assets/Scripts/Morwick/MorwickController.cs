using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorwickController : MonoBehaviour
{
    [Header("Player as Target")]
    private GameObject target;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool direccionLookEnabled = true;
    public bool useRigidBody = false;
    public float rotateSpeed = 10;


    [Header("TargetInView enemy")]
    [SerializeField] Transform viewpoint;
    [SerializeField] float visionToCatch = 5.57f;
    // if morwick see you he will increase the velocity to catch you
    [SerializeField] float visionToSee = 5.57f;
    [SerializeField] LayerMask playerlayer;
    bool targetDirectionLook;
    [HideInInspector] public float timeToForgetPlayer;
    Animator animator;

    Rigidbody rb;

    //------------------------------------------------

    [Header("Pathfinding by points")]
    public bool pathByPointsEnabled = true;
    public List<Transform> points;

    //The int value for the next point in list
    public int nextId;
    // The value of the current point
    int idChargesValue = 1;

    public float speedByFollowPathPoint = 2;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        // Create a root for our enemy
        GameObject root = new GameObject(name + "_Root");
        // Reset position of root to enemy object
        root.transform.position = transform.position;
        // Set the object root as a parent
        transform.SetParent(root.transform);
        // Create a ViewPoint
        GameObject gameObjectViewPoint = new GameObject("ViewPoint");
        // Make the ViewPoint Has a parent from gameObject
        gameObjectViewPoint.transform.SetParent(gameObject.transform);
        viewpoint = gameObjectViewPoint.transform;
        // Create the waypoints
        GameObject wayPoints = new GameObject("WayPoints");
        // Make the gameobject Has a parent to waypoints
        wayPoints.transform.SetParent(root.transform);
        // Set the position waypoint in to root
        wayPoints.transform.position = root.transform.position;
        //Create the points, set the parents to waypoint and set the position for point to root
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(wayPoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(wayPoints.transform); p2.transform.position = root.transform.position;
        // Add the points to the list
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);


    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        target = PlayerController.currentPlayer.gameObject;
    }

    private void FixedUpdate()
    {

        if (!TargetInView(visionToSee) && timeToForgetPlayer <= 0)
        {
            MoveToNextPoint();
        }
        else
        {
            RunTowardsThePlayer();
        }

        if(timeToForgetPlayer >= 0)
        {
            timeToForgetPlayer -= Time.deltaTime;
        }
    }

    void Update()
    {
        if (TargetInView(visionToSee))
        {
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextId];

        // --> Rotation

        Vector3 _direction = (goalPoint.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);

        // <--

        // --> Move

        if (useRigidBody)
        {
            rb.velocity = transform.forward * (speedByFollowPathPoint * 50) * Time.deltaTime;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPoint.position, speedByFollowPathPoint * Time.deltaTime);

        }

        // <--

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(goalPoint.position.x, 0, goalPoint.position.z)) < 0.2f)
        {

            // Check if we are at the end of the line (make the change -1)
            if (nextId == points.Count - 1)
            {
                idChargesValue = -1;
            }
            //Check if we are at the start of the line (make the change +1)
            if (nextId == 0)
            {
                idChargesValue = 1;
            }
            //Apply the change on the nextID
            nextId += idChargesValue;

        }

    }

    void RunTowardsThePlayer()
    {
        Debug.Log("I'm comming for you");

        if (useRigidBody)
        {
            rb.velocity = transform.forward * (speedByFollowPathPoint * 50) * Time.deltaTime;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speedByFollowPathPoint * Time.deltaTime);

        }
    }

    private bool TargetInView(float lenght)
    {
        RaycastHit hit;
        Physics.Raycast(viewpoint.position, viewpoint.forward, out hit, lenght, playerlayer);

        if (hit.collider != null && hit.collider.GetComponent<PlayerController>() != null )
        {
            timeToForgetPlayer = 30f;
            return true;
        }
        return false;
    }

    // OnDrawGizmos
    private void OnDrawGizmos()
    {
        Debug.DrawLine(viewpoint.position, viewpoint.position + viewpoint.forward * visionToCatch);
    }

}
