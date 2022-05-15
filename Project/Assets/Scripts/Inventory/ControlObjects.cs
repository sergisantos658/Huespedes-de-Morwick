using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjects : MonoBehaviour
{
	public List<Items> objetosRecogidos = new List<Items>();

	public void AddObject(Items obj)
	{
		objetosRecogidos.Add(obj);
	}

	public void RemoveObject(Items obj)
	{
		objetosRecogidos.Remove(obj);
	}

	public bool ObjectOn(Items obj)
	{
		return objetosRecogidos.Contains(obj);
	}
}
