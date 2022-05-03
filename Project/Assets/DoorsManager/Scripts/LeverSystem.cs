using UnityEngine;

public class LeverSystem : MonoBehaviour
{
    public bool Locked = false;

    public DoorScript Door;

    public bool isOpened = false;


    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("LeverUp", !Door.isOpened);
    }

    void Update()
    {
        if (!Locked)
        {
            if (Input.GetKeyDown(KeyCode.E) && Door != null && Door.Remote)
            {
                Door.Action();

                anim.SetBool("LeverUp", !Door.isOpened);
            }
        }
    }
}
