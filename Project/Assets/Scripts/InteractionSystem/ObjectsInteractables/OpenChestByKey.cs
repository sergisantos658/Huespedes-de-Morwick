using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestByKey : Interactable
{
    [SerializeField] private GameObject ChestLook;
    [SerializeField] private DialogueObject observation;

    [Space(10)]
    [SerializeField] private Items item;

    bool haveKey = false;
    bool onlyOnce = true;

    private PlayerController player;
    private ControlObjects inventory;
    private void Start()
    {
        player = PlayerController.currentPlayer;
        inventory = player.GetComponent<ControlObjects>();

        if (!inventory.ObjectOn(item) && item.pickUp)
        {
            haveKey = true;

        }
        else
        {
            haveKey = false;
        }
    }

    public override void Interact()
    {
        // Here we need the inventory to search if the inventory have the key
        // Because we don't have it the chest will opened if we just press the chest
        if (inventory.ObjectOn(item))
        {
            haveKey = true;
            inventory.RemoveObject(item);

        }

        if (haveKey)
        {
            ChestLook.GetComponent<Animator>().SetBool("OpenChest", onlyOnce);

            onlyOnce = !onlyOnce;
        }


    }

    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(observation);
    }

}
