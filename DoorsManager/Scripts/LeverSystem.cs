using UnityEngine;

public class LeverSystem : MonoBehaviour
{
    public bool isValve = false;
    public float ValveSpeed = 10f;

    public bool isLever = false;
    public bool Locked = false;

    public DoorScript Door;
    public Transform Ramp;

    public bool CanOpen = true;
    public bool CanClose = true;
    public bool isOpened = false;

    public bool xRotation = true;
    public bool yPosition = false;
    public float max = 90f, min = 0f, speed = 5f;
    bool valveBool = true;
    float current, startYPosition;
    Quaternion startQuat, rampQuat;

    Animator anim;

    float distance;
    float angleView;
    Vector3 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
        startYPosition = Ramp.position.y;
        startQuat = transform.rotation;
        rampQuat = Ramp.rotation;
    }

    void Update()
    {
        if (!Locked)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isValve && Door != null && Door.Remote && NearView())
            {
                Door.Action();

                if (Door.isOpened) anim.SetBool("LeverUp", true);
                else anim.SetBool("LeverUp", false);
            }
            else if (isValve && Ramp != null)
            {
                if (Input.GetKey(KeyCode.E) && NearView())
                {
                    if (valveBool)
                    {
                        if (!isOpened && CanOpen && current < max) current += speed * Time.deltaTime;
                        if (isOpened && CanClose && current > min) current -= speed * Time.deltaTime;

                        if (current >= max)
                        {
                            isOpened = true;
                            valveBool = false;
                        }
                        else if (current <= min)
                        {
                            isOpened = false;
                            valveBool = false;
                        }
                    }
                }
                else
                {
                    if (!isOpened && current > min) current -= speed * Time.deltaTime;
                    if (isOpened && current < max) current += speed * Time.deltaTime;
                    valveBool = true;
                }

                transform.rotation = startQuat * Quaternion.Euler(0f, 0f, current * ValveSpeed);
                if (xRotation) Ramp.rotation = rampQuat * Quaternion.Euler(current, 0f, 0f);
                else if (yPosition) Ramp.position = new Vector3(Ramp.position.x, startYPosition + current, Ramp.position.z);
            }
        }
    }

    bool NearView()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);

        if (angleView < 45f && distance < 2f) return true;
        else return false;
    }
}
