using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickValve : Interactable
{
	//public Transform camKeypad;
	//public Transform camZone;

	//public Collider colliderNumpad;

	public GameObject valve;

	public DialogueObject dialog;

	public static bool valveOn;

	private PlayerController playerC;

    private void Start()
    {
		playerC = PlayerController.currentPlayer;
    }

    void Update()
	{
		//if (camOn)
		//{
		//	activeNum.SetActive(true);
		//}
		//if (Input.GetKeyDown(KeyCode.Return))
		//{
		//	camOn = false;
		//	activeNum.SetActive(false);
		//	Return();
		//}
	}

	public override void Interact()
	{
		valveOn = true;
		valve.SetActive(false);
		//colliderNumpad.enabled = false;
	}

	public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(dialog);
	}
}