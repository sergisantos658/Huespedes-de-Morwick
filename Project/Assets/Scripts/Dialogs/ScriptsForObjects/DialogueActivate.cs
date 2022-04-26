using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivate : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject o_DialogueObject;


    private PlayerController player;

    private void OnEnable()
    {
        PlayerController.WhoIsPlayerController += WhoIsPlayerController;
        DialogueUI.CheckResponseDialogue += ResponseEventsCheck;
    }

    private void OnDisable()
    {
        PlayerController.WhoIsPlayerController -= WhoIsPlayerController;
        DialogueUI.CheckResponseDialogue -= ResponseEventsCheck;
    }

    void WhoIsPlayerController(PlayerController target)
    {
        player = target;
    }

    public override void Interact()
    {
        ResponseEventsCheck(dialogueObject);

        player.DialogueUI.ShowDialogue(dialogueObject);
    }

    public override void Observation()
    {
            player.DialogueUI.ShowDialogue(o_DialogueObject);
    }

    void ResponseEventsCheck(DialogueObject dinamicDialogueObject)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dinamicDialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
    }

    public void UpdateDialogueObject(DialogueObject updateDialogue)
    {
        dialogueObject = updateDialogue;
    }
}
