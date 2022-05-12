using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutValve : Interactable
{
	public Collider colliderHole;

	public GameObject valve;

	public DialogueObject dialogObj;

	private PlayerController playerC;

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
		if (PickValve.valveOn)
		{
			PickValve.valveOn = false;
			valve.SetActive(true);
			colliderHole.enabled = false;
		}
	}

	public override void Observation()
	{
		Debug.Log(dialogObj);
		playerC.DialogueUI.ShowDialogue(dialogObj);
	}
}
