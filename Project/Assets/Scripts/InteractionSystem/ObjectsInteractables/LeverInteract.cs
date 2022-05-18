using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : Interactable
{
    public bool Locked = false;

    public DoorScript Door;

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
        if (!Locked)
        {
            if (Door != null && Door.Remote)
            {
                Door.Lock(false);
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
