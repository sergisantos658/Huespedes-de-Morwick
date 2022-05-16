using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : Interactable
{
    public bool Locked = false;

    public DoorScript Door;

    public bool isOpened = false;

    Animator anim;

    public DialogueObject dialog;

    private PlayerController playerC;

    void Start()
    {
        playerC = PlayerController.currentPlayer;
        anim = GetComponent<Animator>();
        anim.SetBool("LeverUp", !Door.isOpened);
    }

    public override void Interact()
    {
        Debug.Log("f");
        if (!Locked)
        {
            if (Door != null && Door.Remote)
            {
                Door.Action();

                anim.SetBool("LeverUp", !Door.isOpened);
            }
        }
    }

    public override void Observation()
    {
        playerC.DialogueUI.ShowDialogue(dialog);
    }
}
