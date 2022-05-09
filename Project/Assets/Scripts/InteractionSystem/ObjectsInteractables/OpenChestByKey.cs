using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestByKey : Interactable
{
    [SerializeField] private GameObject ChestLook;
    [SerializeField] private DialogueObject observation;

    bool onlyOnce = true;

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
        // Here we need the inventory to search if the inventory have the key
        // Because we don't have it the chest will opened if we just press the chest
        if (player.GetComponent<PlayerInteraction>().BlueKey)
        {
            ChestLook.GetComponent<Animator>().SetBool("OpenChest", true);

        }


    }

    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(observation);
    }

}
