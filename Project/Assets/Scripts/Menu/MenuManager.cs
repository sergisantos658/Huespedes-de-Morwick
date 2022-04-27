using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
	public static bool pause;

	public GameObject mainPauseMenu;

	List<GameObject> menus = new List<GameObject>();

	private void OnEnable()
	{
		ButtonFunction.ChangeMenu += ShowMenu;
	}

	private void OnDisable()
	{
		ButtonFunction.ChangeMenu -= ShowMenu;
	}

	public void ShowMenu(GameObject menu, bool IsPause)
	{
		if (IsPause)
		{
			Time.timeScale = 0;
			pause = true;
			
			
		}

		if (menus.Count - 1 >= 0)
		{
			menus[menus.Count - 1].SetActive(false);
		}

		menus.Add(menu);
		menu.SetActive(true);

	}
	public void HideMenu()
	{
		menus[menus.Count - 1].SetActive(false);
		menus.RemoveAt(menus.Count - 1);
		
		if (menus.Count - 1 >= 0)
		{
			menus[menus.Count - 1].SetActive(true);
		}
		else
        {
			if(PlanetPuzle.Planets == false)
            {
				Time.timeScale = 1;
				pause = false;
			}

		}
	}


	public void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(menus.Count == 0)
            {
				ShowMenu(mainPauseMenu, true);

            }
			else
            {
				HideMenu();
            }
		}
	}
}
