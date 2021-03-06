using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : Interactable
{
    // Start is called before the first frame update
    public DialogueObject close;
    public DialogueObject StartPuzzle;
    ControlObjects controlObjects;
    public Items item;
    public GameObject camera0;
    public GameObject camera1;
    bool finishDialogue;
    bool scenefinish = false;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.currentPlayer;
        controlObjects = player.GetComponent<ControlObjects>();
    }

    public override void Interact()
    {
        if (!CauchObjectLent.keyS3)
        {
            player.DialogueUI.ShowDialogue(close);
        }
        else
        {
            controlObjects.RemoveObject(item);
            player.DialogueUI.ShowDialogue(StartPuzzle);
            finishDialogue = true;


        }
    }
    public void Update()
    {
        if(PlayerCheckPoint.Instance.puzzle3_2 == 0)
        {
            if (!player.DialogueUI.isOpen && finishDialogue == true)
            {
                camera0.SetActive(false);
                camera1.SetActive(true);
                player.gameObject.SetActive(false);
                finishDialogue = false;
            }
            if (CauchObjectLent.keyS3 && piecesScript.puzzleCompleted)
            {
                PlayerCheckPoint.Instance.puzzle3_2 = 1;
                gameObject.SetActive(false);


            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    public override void Observation()
    {

    }
}
