using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivate : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject o_DialogueObject;
    [SerializeField] private DialogueObject MorwickdialgueObject;

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
        if(MorwickController.cauched == false || MorwickdialgueObject == null)
        {
            LookPlayer?.Invoke(player);
            ResponseEventsCheck(dialogueObject);
            player.DialogueUI.ShowDialogue(dialogueObject);
        }
        else
        {
            player.DialogueUI.ShowDialogue(MorwickdialgueObject);
        }

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
    
    public void UpdateObservation(DialogueObject updateDialogue)
    {
        o_DialogueObject = updateDialogue;
    }
}
