using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCinematic : Interactable
{


	public DialogueObject obsBox;
	public DialogueObject normalDialogue;
	private PlayerController playerC;
	public Items item;
	private ControlObjects inventory;
	public ChangeScene creditchange;

	// Start is called before the first frame update
	void Start()
    {
		playerC = PlayerController.currentPlayer;
		inventory = playerC.GetComponent<ControlObjects>();
	}

	// Update is called once per frame
	public override void Interact()
	{
		
		if(!inventory.ObjectOn(item))
		{
			playerC.DialogueUI.ShowDialogue(normalDialogue);
		}
		else
		{
			changesc();

		}
	}
	void changesc()
    {
		creditchange.SceneLoad();
    }
    public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(obsBox);
	}
}
