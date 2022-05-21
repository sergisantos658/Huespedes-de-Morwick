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
	PlayerController player => PlayerController.currentPlayer;
	
	public Animator anim;

	public AudioSource soundCuch;

	public FoldInventory inventory;




	private void Awake()
	{
		Load();
	}
    private void Start()
    {
		inventory = inventory.GetComponent<FoldInventory>();
    }

    private void OnApplicationQuit()
	{
		Save();
	}


	public void AddObject(Items obj)
	{
		obj.pickUp = true;
		soundCuch.Play();
		if(!inventory.isFolded)
        {
			inventory.Action();
		}
		
		objetosRecogidos.Add(obj);
		Save();
		UpdateInventory();
	}

	public void RemoveObject(Items obj)
	{
		obj.pickUp = true;
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
		if(!player.DialogueUI.isOpen)
			player.DialogueUI.ShowDialogue(objetosRecogidos[position].Observation);
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

	
	public void Save()
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
			item.pickUp = true;
			objetosRecogidos.Add(item);
		}

		UpdateInventory();
	}

	void Delete(Items obj)
	{

		string key = ITEMS_KEY;

		int index = objetosRecogidos.IndexOf(obj);

		ItemsData data = SaveItemsSytem.Load<ItemsData>(key + index);
		if (data != null)
		{
			Items items = Resources.Load<Items>(data.scriptedItemName);
			SaveItemsSytem.DeleteFile(key + index);

			Debug.Log("Soy Bobo " + (key + index) + " " + items.pickUp);
		}
	}

}
