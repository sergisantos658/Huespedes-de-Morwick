using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeypad : Interactable
{
	public Transform camKeypad;
	public Transform camZone;

	public GameObject activeNum;

	public DialogueObject dialog;

	public bool camOn;

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

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Return))
        {
			CameraManager.SetCameraPosition(camZone);
		}
    }

	public override void Interact()
	{
		CameraManager.SetCameraPosition(camKeypad);

		if(camOn)
        {
			activeNum.SetActive(true);
		}

		else
        {
			activeNum.SetActive(false);
		}
	}

	public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(dialog);
	}

	public void Action()
	{
		
	}
	
}
