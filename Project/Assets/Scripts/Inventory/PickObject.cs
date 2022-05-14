using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : Interactable
{
	ControlObjects controlObjects;
	public Items item;
	private PlayerController playerC;
	public DialogueObject dialog;

	void Start()
	{
		playerC = PlayerController.currentPlayer;
		controlObjects = playerC.GetComponent<ControlObjects>();
	}

	public override void Interact()
	{
		controlObjects.AddObject(item);
		gameObject.SetActive(false);
	}

	public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(dialog);
	}
}
