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
    public float rotateSpeed = 10;


    [Header("TargetInView enemy")]
    [SerializeField] Transform viewpoint;
    [SerializeField] float lenght = 5.57f;
    [SerializeField] LayerMask playerlayer;
    bool targetDirectionLook;
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
        targetDirectionLook = TargetInView();

        if (followEnabled)
        {
            MoveToNextPoint();
        }
    }

    void Update()
    {
        if (targetDirectionLook)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Walk", true);
        }
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextId];

        // -->
        //Vector3 relative = transform.InverseTransformPoint(goalPoint.position);
        //float angle = Mathf.Atan2(goalPoint.position.x, goalPoint.position.z) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, angle -90 , 0);
        // <--

        // --> 
        Vector3 targetDir = goalPoint.position - transform.position;
        Vector3 forward = transform.forward;

        float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);
        float angleToApply = (angle > 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime;
        if (
            ((angle > 0) && (angleToApply > angle)) ||
            ((angle < 0) && (angleToApply < angle))
            )
        { angleToApply = angle; }

        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
        // <--

        transform.position = Vector3.MoveTowards(transform.position, goalPoint.position, speedByFollowPathPoint * Time.deltaTime);
        //rb.MovePosition(goalPoint.position * speedByFollowPathPoint * Time.deltaTime);


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

    private bool TargetInView()
    {
        RaycastHit hit;
        Physics.Raycast(viewpoint.position, Vector3.forward, out hit, lenght, playerlayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    // OnDrawGizmos
    private void OnDrawGizmos()
    {
        Debug.DrawLine(viewpoint.position, viewpoint.position + Vector3.forward * lenght);
    }

}
