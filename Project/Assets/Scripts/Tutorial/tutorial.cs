using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerC;
    public DialogueObject tuto;
    private static bool tutoStart = true;

    private void Start()
    {
        playerC = PlayerController.currentPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tutoStart == true)
        {
            playerC.DialogueUI.ShowDialogue(tuto);
            tutoStart = false;
        }

    }
}
