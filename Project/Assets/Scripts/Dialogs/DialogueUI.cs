using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    [SerializeField] private GameObject silhouettePlayer;
    [SerializeField] private GameObject silhouetteNPC;

    Image iSilhouettePlayer;
    Image iSilhouetteNPC;

    public static event Action StopDialogue = delegate { };

    public bool isOpen { get; private set; }

    private ResponsableHandler responseHandler;
    private TypewritterEffect typewriterEffect;

    void Start()
    {
        typewriterEffect = GetComponent<TypewritterEffect>();
        responseHandler = GetComponent<ResponsableHandler>();

        silhouettePlayer.SetActive(false);
        silhouetteNPC.SetActive(false);

        iSilhouettePlayer = silhouettePlayer.GetComponent<Image>();
        iSilhouetteNPC = silhouetteNPC.GetComponent<Image>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject == null) return;

        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.DialogueTyping.Length; i++)
        {
            // Image conversation
            if((int)dialogueObject.DialogueTyping[i].whoTalk == 0)
            {
                if (!silhouettePlayer.activeInHierarchy) silhouettePlayer.SetActive(true);

                iSilhouettePlayer.color = new Color(iSilhouettePlayer.color.r, iSilhouettePlayer.color.g, iSilhouettePlayer.color.b, 1);

                iSilhouetteNPC.color = new Color(iSilhouetteNPC.color.r, iSilhouetteNPC.color.g, iSilhouetteNPC.color.b, 0.4f);
            }
            else
            {
                if (!silhouetteNPC.activeInHierarchy) silhouetteNPC.SetActive(true);

                iSilhouetteNPC.color = new Color(iSilhouetteNPC.color.r, iSilhouetteNPC.color.g, iSilhouetteNPC.color.b, 1);

                iSilhouettePlayer.color = new Color(iSilhouettePlayer.color.r, iSilhouettePlayer.color.g, iSilhouettePlayer.color.b, 0.4f);
            }

            // Text
            for (int g = 0; g < dialogueObject.DialogueTyping[i].dialogue.Length; g++)
            {
                string dialogue = dialogueObject.DialogueTyping[i].dialogue[g];

                yield return RunTypingEffect(dialogue);

                textLabel.text = dialogue;

                if (i == dialogueObject.DialogueTyping[i].dialogue.Length - 1 && dialogueObject.HasResponses) break;

                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }

    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.isRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        StopDialogue();
        silhouettePlayer.SetActive(false);
        silhouetteNPC.SetActive(false);
    }
}
