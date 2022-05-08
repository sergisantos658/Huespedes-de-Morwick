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
    bool scenefinish = false;

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
        if (!CauchObjectLent.keyS3)
        {
            player.DialogueUI.ShowDialogue(close);
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
        if (CauchObjectLent.keyS3 && piecesScript.puzzleCompleted)
        {
                gameObject.AddComponent<DoorInteraction>();
                gameObject.GetComponent<DoorInteraction>().openDialgue = finishscene;
                Destroy(this);
            

        }
    }
    public override void Observation()
    {

    }
}
