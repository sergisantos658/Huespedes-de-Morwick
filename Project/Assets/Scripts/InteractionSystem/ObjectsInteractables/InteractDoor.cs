using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : Interactable
{
    // Start is called before the first frame update
    public DialogueObject close;
    public DialogueObject StartPuzzle;
    public DialogueObject finishscene;
    public GameObject camera0;
    public GameObject camera1;
    bool finishDialogue;

    private PlayerController player;
    private void OnEnable()
    {
        PlayerController.WhoIsPlayerController += WhoIsPlayerController;
    }

    private void OnDisable()
    {
        PlayerController.WhoIsPlayerController -= WhoIsPlayerController;
    }

    void WhoIsPlayerController(PlayerController target)
    {
        player = target;
    }

    public override void Interact()
    {
        if(!CauchObjectLent.keyS3)
        {
            player.DialogueUI.ShowDialogue(close);
        }
        else if(CauchObjectLent.keyS3 && piecesScript.puzzleCompleted)
        {
            player.DialogueUI.ShowDialogue(finishscene);
        }
        else
        {
            player.DialogueUI.ShowDialogue(StartPuzzle);
            finishDialogue = true;


        }
    }
    public void Update()
    {
        if (!player.DialogueUI.isOpen && finishDialogue == true)
        {
            camera0.SetActive(false);
            camera1.SetActive(true);
            player.gameObject.SetActive(false);
            finishDialogue = false;
        }
    }
    public override void Observation()
    {

    }
}
