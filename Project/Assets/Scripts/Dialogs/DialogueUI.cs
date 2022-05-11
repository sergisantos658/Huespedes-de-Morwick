using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

using UnityEditor;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    [SerializeField] private GameObject silhouettePlayer;
    [SerializeField] private GameObject silhouetteNPC;

    Image iSilhouettePlayer;
    Image iSilhouetteNPC;

    public static event Action StopDialogue = delegate { };
    public static event Action<DialogueObject> CheckResponseDialogue = delegate { };

    public bool isOpen { get; private set; }

    private ResponsableHandler responseHandler;
    private TypewritterEffect typewriterEffect;

    private Vector2 normalCursorHotspot;
    void Start()
    {
        typewriterEffect = GetComponent<TypewritterEffect>();
        responseHandler = GetComponent<ResponsableHandler>();

        silhouettePlayer.SetActive(false);
        silhouetteNPC.SetActive(false);

        iSilhouettePlayer = silhouettePlayer.GetComponent<Image>();
        iSilhouetteNPC = silhouetteNPC.GetComponent<Image>();
        normalCursorHotspot = new Vector2(20, 10);

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject == null) return;

        isOpen = true;
        dialogueBox.SetActive(true);
        Cursor.SetCursor(PlayerSettings.defaultCursor, normalCursorHotspot, CursorMode.ForceSoftware);
        if (PlayerInteraction.interactableDinamic != null) PlayerInteraction.interactableDinamic = null;
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

        // Check if has some responses
        if (dialogueObject.HasResponses)
        {
            //TODO Put an event which check if someone in the event have the same dialogueObject like dialogueUI
            CheckResponseDialogue.Invoke(dialogueObject);

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
