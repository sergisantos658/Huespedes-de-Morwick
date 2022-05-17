using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerC;
    public DialogueObject tuto;
    public static bool tutoStart = true;

    private void Start()
    {
        playerC = PlayerController.currentPlayer;
        tutoStart = PlayerCheckPoint.Instance.tutorial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (tutoStart == true /*&& !TimeLineManager.isPlaying*/)
        {
            playerC.DialogueUI.ShowDialogue(tuto);
            tutoStart = false;
            PlayerCheckPoint.Instance.CheckPointSave();
        }

    }
}
