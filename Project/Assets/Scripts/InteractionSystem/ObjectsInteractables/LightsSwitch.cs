using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightsSwitch : Interactable
{
    [Header("Default ")]
    public GameObject switchButton;
    public DialogueObject defaultDialogue;
    public DialogueObject getSwitchCoverDialogue;

    [Space(20)]
    public DialogueObject PressOffTheLightSwitchDialogue;



    [Header("Item ")]
    [SerializeField] private Items lightSwitchItem;

    [Header("Lights ")]
    [SerializeField] private GameObject[] m_Lights;

    [SerializeField] private bool isLightSwitch = false;
    public bool allLightsOff = false;

    private bool onlyOnce = false;
    private bool lights = false;
    private PlayerController player;
    private ControlObjects inventory;

    private void OnEnable()
    {
        DialogueUI.CheckResponseDialogue += ResponseEventsCheck;
    }

    private void OnDisable()
    {
        DialogueUI.CheckResponseDialogue -= ResponseEventsCheck;
    }

    private void Start()
    {
        player = PlayerController.currentPlayer;
        inventory = player.GetComponent<ControlObjects>();

        if (PlayerCheckPoint.Instance.puzzle2 == 0)
        {
            isLightSwitch = false;
        }
        else if(PlayerCheckPoint.Instance.puzzle2 == 1)
        {
            isLightSwitch = true;
            UpdateDialogueObject(PressOffTheLightSwitchDialogue);
            CheckInInventory();
            switchButton.SetActive(true);
            return;
        }
        
        if (inventory.ObjectOn(lightSwitchItem))
        {
            isLightSwitch = true;
        }
        else if (PlayerPrefs.GetInt(lightSwitchItem.name) == 1 || lightSwitchItem.pickUp && !inventory.ObjectOn(lightSwitchItem))
        {
            isLightSwitch = true;
            switchButton.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt(lightSwitchItem.name, 0);
        }


    }

    public void CheckInInventory()
    {
        if (inventory.ObjectOn(lightSwitchItem))
        {
            PlayerPrefs.SetInt(lightSwitchItem.name, 1);
            inventory.RemoveObject(lightSwitchItem);
        }
    }

    public void UpdateLight()
    {
        if (!onlyOnce)
        {
            allLightsOff = true;

            for (int i = 0; i < m_Lights.Length; i++)
            {
                int count = m_Lights[i].transform.childCount;
                bool baked = true;
                for (int o = 0; o < count; o++)
                {
                    if (m_Lights[i].transform.GetChild(o).GetComponent<Light>())
                    {
                        if (!m_Lights[i].transform.GetChild(o).GetComponent<Light>().bakingOutput.isBaked)
                        {
                            baked = false;  
                        }
                    }

                    if(!baked && m_Lights[i].transform.GetChild(o).GetComponent<Light>())
                    {
                        m_Lights[i].transform.GetChild(o).GetComponent<Light>().enabled = !m_Lights[i].transform.GetChild(o).GetComponent<Light>().enabled;

                        Behaviour halo = (Behaviour)m_Lights[i].transform.GetChild(o).GetComponent("Halo");
                        halo.enabled = false;

                        if (m_Lights[i].transform.GetChild(o).GetComponent<Light>().enabled == true)
                        {
                            allLightsOff = false;
                        }
                    }
                    else if (!baked)
                    {
                        Behaviour halo = (Behaviour)m_Lights[i].transform.GetChild(o).GetComponent("Halo");
                        halo.enabled = false;
                        m_Lights[i].transform.GetChild(o).gameObject.SetActive(lights);
                    }
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
        if (isLightSwitch)
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