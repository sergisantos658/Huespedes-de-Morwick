using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool Locked = false;
    public bool Remote = false;

    public bool CanOpen = true;
    public bool CanClose = true;

    public bool RedLocked = false;
    public bool BlueLocked = false;

    PlayerInteractive playerInteractive;
    [Space]
    public bool isOpened = false;
    [Range(0f, 4f)]

    public float OpenSpeed = 3f;


    float distance;
    float angleView;
    Vector3 direction;

    [HideInInspector]
    public Rigidbody rbDoor;
    HingeJoint hinge;
    JointLimits hingeLim;
    float currentLim;

    void Start()
    {
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        playerInteractive = FindObjectOfType<PlayerInteractive>();
    }

    void Update()
    {
        if ( !Remote && Input.GetKeyDown(KeyCode.E) && NearView() )
            Action();
        
    }

    public void Action()
    {
        if (!Locked)
        {
            if (playerInteractive != null && RedLocked && playerInteractive.RedKey)
            {
                RedLocked = false;
                playerInteractive.RedKey = false;
            }
            else if (playerInteractive != null && BlueLocked && playerInteractive.BlueKey)
            {
                BlueLocked = false;
                playerInteractive.BlueKey = false;
            }
            
            if (isOpened && CanClose && !RedLocked && !BlueLocked)
            {
                isOpened = false;
            }
            else if (!isOpened && CanOpen && !RedLocked && !BlueLocked)
            {
                isOpened = true;
                rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f)); 
            }
        
        }
    }

    bool NearView()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 3f) return true;
        else return false;
    }

    private void FixedUpdate()
    {
        if (isOpened)
        {
            currentLim = 85f;
        }
        else
        {
            if (currentLim > 1f)
                currentLim -= 0.5f * OpenSpeed;
        }

        hingeLim.max = currentLim;
        hingeLim.min = -currentLim;
        hinge.limits = hingeLim;
    }
}
