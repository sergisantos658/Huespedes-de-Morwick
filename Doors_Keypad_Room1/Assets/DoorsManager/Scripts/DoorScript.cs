using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	public bool Locked = false;
	public bool Remote = false;

	public bool CanOpen = true;
	public bool CanClose = true;

	public bool RedLocked = false;
	public bool BlueLocked = false;

	PlayerInteractive playerInteractive;

	public bool isOpened = false;

	public float OpenSpeed = 3f;

	Animator anim;

	public bool useAnimation;

	void Start()
	{
		anim = GetComponent<Animator>();
		playerInteractive = FindObjectOfType<PlayerInteractive>();
	}

	void Update()
	{
		if ( !Remote && Input.GetKeyDown(KeyCode.E))
			Action();
	}

	public void Action()
	{
		if (!Locked)
		{
			//if (playerInteractive != null && RedLocked && playerInteractive.RedKey)
			//{
			//	RedLocked = false;
			//	playerInteractive.RedKey = false;
			//}
			//else if (playerInteractive != null && BlueLocked && playerInteractive.BlueKey)
			//{
			//	BlueLocked = false;
			//	playerInteractive.BlueKey = false;
			//}

			//if (isOpened && CanClose && !RedLocked && !BlueLocked)
			//{
			//	isOpened = false;
			//}
			//else if (!isOpened && CanOpen && !RedLocked && !BlueLocked)
			//{
			//	isOpened = true;
			//	rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f)); 
			//}
			isOpened = !isOpened;
			anim.SetBool("opened", isOpened);
		}
	}
}
