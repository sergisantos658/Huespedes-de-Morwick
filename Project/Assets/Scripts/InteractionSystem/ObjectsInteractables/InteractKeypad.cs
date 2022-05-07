using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeypad : Interactable
{
	public Transform camKeypad;
	public Transform camZone;

	public Collider colliderNumpad;

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
		if (camOn)
		{
			activeNum.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Return))
        {
			camOn = false;
			activeNum.SetActive(false);
			Return();
		}
    }

	public override void Interact()
	{
		CameraManager.SetCameraPosition(camKeypad);

		camOn = true;
		colliderNumpad.enabled = false;
	}

	public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(dialog);
	}

	public void Return()
	{
		colliderNumpad.enabled = true;
		CameraManager.SetCameraPosition(camZone);
	}
	
}
