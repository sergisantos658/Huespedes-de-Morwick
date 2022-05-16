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

	public bool camOn = false;

	private PlayerController playerC;

    private void Start()
    {
		playerC = PlayerController.currentPlayer;
    }

    void Update()
    {
		if (camOn)
		{
			activeNum.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Escape) && camOn)
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
		PlayerController.currentPlayer.gameObject.SetActive(false);
	}

	public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(dialog);
	}

	public void Return()
	{
		colliderNumpad.enabled = true;
		CameraManager.SetCameraPosition(camZone);
		PlayerController.currentPlayer.gameObject.SetActive(true);
	}
	
}
