using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeypad : Interactable
{
	public Transform camPos;
	public GameObject activeNum;
	public bool camOn;
	void Start()
	{
	   
	}

	public override void Interact()
	{
		CameraManager.SetCameraPosition(camPos);

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

	}

	public void Action()
	{
		
	}
	
}
