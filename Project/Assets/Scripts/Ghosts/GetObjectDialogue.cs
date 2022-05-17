using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectDialogue : MonoBehaviour
{
    ControlObjects controlObjects;
    public Items item;
    private PlayerController playerC;

    void Start()
    {
		playerC = PlayerController.currentPlayer;
		controlObjects = playerC.GetComponent<ControlObjects>();
	}

    public void GetObject()
    {
        controlObjects.AddObject(item);
    }
}
