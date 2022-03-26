using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    public DialogueObject obs;
    public Light m_Light; // im using m_Light name since 'light' is already a variable used by unity

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

    private void Start()
    {
        UpdateLight();
    }

    void UpdateLight()
    {
        m_Light.enabled = !m_Light.enabled;
    }

    public override void Interact()
    {
        UpdateLight();
    }
    public override void Observation()
    {
        player.DialogueUI.ShowDialogue(obs);
    }
}
