using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum InteractionOptions
{
    Interactable,
    Observation,
    Both,
    none
}

public class TextToInteractableOrObservation : MonoBehaviour
{
	public TextMeshProUGUI m_text;

    [HideInInspector]


    private void OnEnable()
    {
        PlayerInteraction.TextThatInteract += CheckWhatInteractionIs;
    }

    private void OnDisable()
    {
        PlayerInteraction.TextThatInteract -= CheckWhatInteractionIs;
    }

    public void CheckWhatInteractionIs(InteractionOptions interactOption)
    {
		if(interactOption == InteractionOptions.Both)
        {
            m_text.text = "Interactuable y Observable";
        }
        else if (interactOption == InteractionOptions.Interactable)
        {
            m_text.text = "Interactuable";
        }
        else if (interactOption == InteractionOptions.Observation)
        {
            m_text.text = "Observable";
        }
        else if(interactOption == InteractionOptions.none)
        {
            m_text.text = " ";
        }

	}
}
