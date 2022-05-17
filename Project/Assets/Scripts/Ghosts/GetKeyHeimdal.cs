using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyHeimdal : MonoBehaviour
{
    ControlObjects controlObjects;
    public Items item;
    private PlayerController playerC;

    void Start()
    {
		playerC = PlayerController.currentPlayer;
		controlObjects = playerC.GetComponent<ControlObjects>();
	}

    public void GetKey()
    {
        controlObjects.AddObject(item);
    }
}
