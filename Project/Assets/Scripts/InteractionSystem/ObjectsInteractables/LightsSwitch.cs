using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightsSwitch : Interactable
{
    public DialogueObject defaultDialogue;
    public DialogueObject getSwitchCoverDialogue;

    [SerializeField] private GameObject[] m_Lights;

    [SerializeField] private bool lightSwitch = false;
    public bool allLightsOff = false;

    private bool onlyOnce = false;
    private bool lights = false;
    private PlayerController player;

    private void Start()
    {
        player = PlayerController.currentPlayer;
    }

    public void UpdateLight()
    {
        if (!onlyOnce)
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
                    m_light.SetActive(lights);
                }

            }

            lights = !lights;

            onlyOnce = true;
        }
        
    }

    public void UpdateDialogueObject(DialogueObject newDialogue)
    {
        getSwitchCoverDialogue = newDialogue;
    }

    void ResponseEventsCheck(DialogueObject dinamicDialogueObject)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dinamicDialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
    }

    public override void Interact()
    {
        if (lightSwitch)
        {
            onlyOnce = false;

            ResponseEventsCheck(getSwitchCoverDialogue);

            player.DialogueUI.ShowDialogue(getSwitchCoverDialogue);
        }
        else
        {
            player.DialogueUI.ShowDialogue(defaultDialogue);
        }
    }

    public override void Observation()
    {

    }
}