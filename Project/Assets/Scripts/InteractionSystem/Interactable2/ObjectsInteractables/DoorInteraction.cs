using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactable
{
    public bool isRedKey = true;

    float distance;
    float angleView;
    Vector3 direction;

    [SerializeField] private DialogueObject NotCompleted;
    [SerializeField] private DialogueObject observation;

    private PlayerController player;
    private PlayerInteraction pInteract;
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
        if (isRedKey) pInteract.RedKey = true;
        else pInteract.BlueKey = true;
        Destroy(gameObject);
    }

    public override void Observation()
    {
       
    }
}
