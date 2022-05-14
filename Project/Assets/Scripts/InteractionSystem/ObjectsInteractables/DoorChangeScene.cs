using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeScene : Interactable
{
    [SerializeField] private DialogueObject NotCompleted;
    [SerializeField] private DialogueObject observation;
    [SerializeField, Range(-1,1)] private int jumpIntoScene;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.currentPlayer;
    }

    public override void Interact()
    {
        if(SceneManager.GetActiveScene().buildIndex + jumpIntoScene < 0)
        {
            Application.Quit();
        }

        if(jumpIntoScene != 0)
        {
            //player.GetComponent<PlayerCheckPoint>().CheckPointSave();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + jumpIntoScene);

        }
        else
        {
            player.DialogueUI.ShowDialogue(NotCompleted);
        }
    }

    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(observation);
    }

}
