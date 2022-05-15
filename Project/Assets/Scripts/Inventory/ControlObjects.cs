using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlObjects : MonoBehaviour
{
	public List<Items> objetosRecogidos = new List<Items>();

	const string ITEMS_KEY = "/items";
	const string ITEMS_Count_KEY = "/items.count";

	private void Awake()
	{
		Load();
	}

	private void OnApplicationQuit()
	{
		Save();
	}


	public void AddObject(Items obj)
	{
		objetosRecogidos.Add(obj);
		Save();
	}

	public void RemoveObject(Items obj)
	{
		objetosRecogidos.Remove(obj);
		Save();
	}

	public bool ObjectOn(Items obj)
	{
		return objetosRecogidos.Contains(obj);
	}

	void Save()
	{
		string key = ITEMS_KEY;
		string countKey = ITEMS_Count_KEY;

		SaveItemsSytem.Save(objetosRecogidos.Count, countKey);

		for (int i = 0; i < objetosRecogidos.Count; i++)
		{
			ItemsData data = new ItemsData(objetosRecogidos[i]);
			SaveItemsSytem.Save(data, key + i);
		}
	}

	void Load()
	{
		objetosRecogidos.Clear();

		string key = ITEMS_KEY;
		string countKey = ITEMS_Count_KEY;
		int count = SaveItemsSytem.Load<int>(countKey);

		for (int i = 0; i < count; i++)
		{
			ItemsData data = SaveItemsSytem.Load<ItemsData>(key + i);
			Items item = Resources.Load<Items>(data.scriptedItemName);
			objetosRecogidos.Add(item);
		}
	}

}
