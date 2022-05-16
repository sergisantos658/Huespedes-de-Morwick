using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (item.pickUp)
        {
			gameObject.SetActive(false);
			return;
        }

		if (controlObjects != null)
		{
			for (int i = 0; i < controlObjects.objetosRecogidos.Count; i++)
			{
				if (controlObjects.objetosRecogidos[i] == item)
				{
					gameObject.SetActive(false);
					return;
				}
			}
		}
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
