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
        if(jumpIntoScene != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + jumpIntoScene);
        else
            player.DialogueUI.ShowDialogue(NotCompleted);
    }

    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(observation);
    }

}
