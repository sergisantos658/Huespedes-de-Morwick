using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDefault : Interactable
{
    [SerializeField] private DialogueObject dialogueObject;

    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public override void Interact()
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
    public override void Observation()
    {

    }
}
