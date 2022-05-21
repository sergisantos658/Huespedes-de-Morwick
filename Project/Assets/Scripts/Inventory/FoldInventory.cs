using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldInventory : MonoBehaviour
{
	public bool isFolded = false;

	Animator anim;


	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse2))
		{
			Action();
		}
	}

	public void Action()
	{
		//Debug.Log("snif");
		isFolded = !isFolded;
		anim.SetBool("folded", isFolded);
	}



	//public Vector2 foldedPos;
	//Vector2 originalPos;
	//public float foldingTime;

	//[HideInInspector]
	//public bool isFolded = false;

	//void Start()
	//{
	//	originalPos = transform.position;
	//	isFolded = false;
	//}

	//void Update()
	//   {


	//	if(Input.GetKeyDown(KeyCode.Mouse2))
	//       {
	//		FoldInv();
	//	}
	//   }

	//public void FoldInv()
	//{
	//	StartCoroutine(Fold());
	//}

	//IEnumerator Fold()
	//{
	//	for (float time = 0f; time < foldingTime; time += Time.deltaTime)
	//	{
	//		if(!isFolded)
	//           {
	//			transform.position = Vector2.Lerp(originalPos, foldedPos, time / foldingTime);
	//		}

	//		else
	//		{
	//			transform.position = Vector2.Lerp(foldedPos, originalPos, time / foldingTime);
	//		}

	//		yield return new WaitForEndOfFrame();
	//	}

	//	isFolded = !isFolded;
	//}
}
