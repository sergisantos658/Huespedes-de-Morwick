using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlObjects : MonoBehaviour
{
	public List<Items> objetosRecogidos = new List<Items>();
	public List<Image> images = new List<Image>();

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
		obj.pickUp = true;
		objetosRecogidos.Add(obj);
		Save();
		UpdateInventory();
	}

	public void RemoveObject(Items obj)
	{
		objetosRecogidos.Remove(obj);
		Save();
		UpdateInventory();
	}

	public bool ObjectOn(Items obj)
	{
		return objetosRecogidos.Contains(obj);
	}

	public void DialogItem(int position)
	{
		PlayerController.currentPlayer.DialogueUI.ShowDialogue(objetosRecogidos[position].Observation);
	}

	public void UpdateInventory()
	{
		for (int i = 0; i < images.Count; i++)
		{
			if(i < objetosRecogidos.Count)
            {
				images[i].gameObject.SetActive(true);
				images[i].sprite = objetosRecogidos[i].spriteInventory;
			}
			else
            {
				images[i].gameObject.SetActive(false);
            }
		}
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

		UpdateInventory();
	}
	  

}
