using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	public bool Locked = false;
	public bool Remote = false;

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

	public void Action()
	{
		if (!Locked)
		{
			isOpened = !isOpened;
			anim.SetBool("opened", isOpened);
		}
	}

	public void Lock(bool value)
    {
		Locked = value;
	}
}
