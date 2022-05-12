using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutVave : Interactable
{
    public Collider colliderHole;

    public GameObject valve;

    public DialogueObject dialogObj;

    private PlayerController playerC;

    void Start()
    {
        playerC = PlayerController.currentPlayer;
    }

    public override void Interact()
    {
		if (PickValve.valveOn)
		{
			PickValve.valveOn = false;
			valve.SetActive(true);
			colliderHole.enabled = false;
		}
	}

    public override void Observation()
    {
        playerC.DialogueUI.ShowDialogue(dialogObj);
    }
}