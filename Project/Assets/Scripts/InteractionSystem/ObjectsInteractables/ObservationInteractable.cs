using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationInteractable : Interactable
{
    public DialogueObject observDialogue;

    private PlayerController player;
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
        player = target;
    }

    public override void Interact()
    {
        
    }

    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(observDialogue);
    }
}
