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

		if (PlayerCheckPoint.Instance.puzzle1 == 0)
		{
			valve.SetActive(false);
		}
		else if (PlayerCheckPoint.Instance.puzzle1 == 1)
		{
			valve.SetActive(true);
			colliderHole.enabled = false;
			return;
		}

		if (PlayerPrefs.GetInt(item.name) == 1 ||  item.pickUp && !inventory.ObjectOn(item))
        {
			valve.SetActive(true);
			colliderHole.enabled = false;
        }
        else
        {
			PlayerPrefs.SetInt(item.name, 0);
        }
	}

	public override void Interact()
	{
		if(inventory.ObjectOn(item))
		{
			PlayerPrefs.SetInt(item.name, 1);
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
