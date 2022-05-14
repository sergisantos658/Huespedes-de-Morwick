using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivate : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject o_DialogueObject;

    private PlayerController player => PlayerController.currentPlayer;
    public  event Action<PlayerController> LookPlayer = delegate { };

    private void OnEnable()
    {
        DialogueUI.CheckResponseDialogue += ResponseEventsCheck;
    }

    private void OnDisable()
    {
        DialogueUI.CheckResponseDialogue -= ResponseEventsCheck;
    }

    public override void Interact()
    {

        LookPlayer?.Invoke(player);
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
