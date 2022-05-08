using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightsSwitch : Interactable
{
    public DialogueObject defaultDialogue;

    [SerializeField] private GameObject[] m_Lights;

    [SerializeField] private bool lightSwitch = false;
    public bool allLightsOff = false;

    private bool onlyOnce = false;
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

    void UpdateLight()
    {
        if (lightSwitch)
        {
            allLightsOff = true;

            foreach (GameObject m_light in m_Lights)
            {

                if (m_light.GetComponent<Light>())
                {
                    m_light.GetComponent<Light>().enabled = !m_light.GetComponent<Light>().enabled;
                    if (m_light.GetComponent<Light>().enabled == true) 
                    { 
                        allLightsOff = false; 
                    }
                }
                else
                {
                    m_light.SetActive(onlyOnce);
                }

            }

            onlyOnce = !onlyOnce;

        }
        else
        {
            player.DialogueUI.ShowDialogue(defaultDialogue);
        }
        
    }

    public override void Interact()
    {
        UpdateLight();
    }
    public override void Observation()
    {

    }
}