using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightSwitchText : Interactable
{
    public DialogueObject defaultDialogue;
    public DialogueObject observation;
    [SerializeField] private GameObject[] m_Lights;
    [SerializeField] private TextMeshPro[] numbersText;

    [SerializeField] private bool lightSwitch = false;
    [SerializeField] private bool allLightsOff = false;

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
                //m_light.enabled = !m_light.enabled;

                if (!m_light.activeInHierarchy)
                {
                    m_light.gameObject.SetActive(true);
                    allLightsOff = false;
                }
                else
                {
                    m_light.gameObject.SetActive(false);
                }
            }
            
            foreach (TextMeshPro number in numbersText)
            {
                number.color = new Color(number.color.r, number.color.g, number.color.b, allLightsOff ? 1 : 0);            
            }
            

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
        player.DialogueUI.ShowDialogue(observation);
    }
}