using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutValve : Interactable
{
	public Collider colliderHole;

	public GameObject valve;

	public DialogueObject dialogObj;

	private ControlObjects inventory;
	private PlayerController player => PlayerController.currentPlayer;

	public Items item;

	void Start()
	{
		inventory = player.GetComponent<ControlObjects>();

		if(item.pickUp && !inventory.ObjectOn(item))
        {
			valve.SetActive(true);
			colliderHole.enabled = false;
		}
	}

	public override void Interact()
	{
		if(inventory.ObjectOn(item))
		{
			inventory.RemoveObject(item);
			valve.SetActive(true);
			colliderHole.enabled = false;
		}
	}

	public override void Observation()
	{
		player.DialogueUI.ShowDialogue(dialogObj);
	}
}
