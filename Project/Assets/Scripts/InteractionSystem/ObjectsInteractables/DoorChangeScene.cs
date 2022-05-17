using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeScene : Interactable
{
    [SerializeField] private DialogueObject NotCompleted;
    [SerializeField] private DialogueObject observation;
    [SerializeField, Range(-1,1)] private int jumpIntoScene;
    [SerializeField] private TYPEPUZZLE type;
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

            switch (type)
            {
                case TYPEPUZZLE.puzzle1:
                    PlayerCheckPoint.Instance.puzzle1 = 1;
                    break;
                case TYPEPUZZLE.puzzle2:
                    PlayerCheckPoint.Instance.puzzle2 = 1;
                    break;
                case TYPEPUZZLE.puzzle3_1:
                    PlayerCheckPoint.Instance.puzzle3_1 = 1;
                    break;
                case TYPEPUZZLE.puzzle3_2:
                    PlayerCheckPoint.Instance.puzzle3_2 = 1;
                    break;
                case TYPEPUZZLE.none:
                    //Debug.LogError("None puzzle has been selected");
                    break;
                default:
                    break;
            }
            player.GetComponent<PlayerCheckPoint>().CheckPointSave();

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
