using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjects : MonoBehaviour
{
	public List<GameObject> objetosRecogidos = new List<GameObject>();

	public void AddObject(GameObject obj)
	{
		if(ObjectOn(obj))
		{
			LeftObject(obj);
		}
		else
		{
			objetosRecogidos.Add(obj);
		}
	}

	public void LeftObject(GameObject obj)
	{
		objetosRecogidos.Remove(obj);
	}

	public bool ObjectOn(GameObject obj)
	{
		return objetosRecogidos.Contains(obj);
	}
}
