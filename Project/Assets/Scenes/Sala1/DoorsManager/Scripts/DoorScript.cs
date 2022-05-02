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

	PlayerInteraction playerInteractive;

	public bool isOpened = false;

	public float OpenSpeed = 3f;

	Animator anim;

	public bool useAnimation;

	void Start()
	{
		anim = GetComponent<Animator>();
		playerInteractive = FindObjectOfType<PlayerInteraction>();
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
			isOpened = !isOpened;
			anim.SetBool("opened", isOpened);
		}
	}
}
