using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactable
{
    public bool Locked = false;
    public bool Remote = false;

    public bool isOpened = false;

    public DialogueObject openDialgue;
    public DialogueObject closeDialgue;

    private PlayerController playerC;

    public float OpenSpeed = 3f;

    Animator anim;

    public bool useAnimation;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerController.WhoIsPlayerController += WhoIsPlayerController;
    }

    private void OnDisable()
    {
        PlayerController.WhoIsPlayerController -= WhoIsPlayerController;
    }

    void WhoIsPlayerController(PlayerController target)
    {
        playerC = target;
    }

    public override void Interact()
    {
        if (!Remote)
            Action();
    }

    public override void Observation()
    {
       if(Locked == true && closeDialgue != null)
        {
            playerC.DialogueUI.ShowDialogue(closeDialgue);
        }
       else if(Locked == false && openDialgue != null)
        {
            playerC.DialogueUI.ShowDialogue(openDialgue);
        }
    }

    public void Action()
    {
        if (!Locked)
        {
            isOpened = !isOpened;
            anim.SetBool("opened", isOpened);
        }
    }
}
