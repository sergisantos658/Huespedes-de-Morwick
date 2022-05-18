using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	public bool Locked = false;
	public bool Remote = false;

	[Header("Scene that you are")]
	[SerializeField] private TYPEPUZZLE type;

	public bool isOpened = false;

	Animator anim;

	public bool useAnimation;

	void Start()
	{
		anim = GetComponent<Animator>();
		if (((type == TYPEPUZZLE.puzzle1 && PlayerCheckPoint.Instance.puzzle1 > 0) ||
			(type == TYPEPUZZLE.puzzle2 && PlayerCheckPoint.Instance.puzzle2 > 0) ||
			(type == TYPEPUZZLE.puzzle3_1 && PlayerCheckPoint.Instance.puzzle3_1 > 0) ||
			(type == TYPEPUZZLE.puzzle3_2 && PlayerCheckPoint.Instance.puzzle3_2 > 0)) &&
			type != TYPEPUZZLE.none)
				Locked = false;
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

	public void Lock(bool value)
    {
		Locked = value;
	}
}
