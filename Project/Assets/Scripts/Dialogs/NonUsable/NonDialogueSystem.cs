using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NonDialogueSystem : MonoBehaviour
{
    public static NonDialogueSystem instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject NPC;
    [SerializeField] private TMP_Text dialogueText;

    private string[] dialogueLines;
    private NonDialogue[] dialogues;

    bool didDialogueStart = false;
    public bool finishDialog = false;
    int dialogIndex;
    int lineIndex;
    float typingTime = 0.05f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);

    }

    public void Update()
    {
        if (didDialogueStart == true)
        {
            Time.timeScale = 0f;
        }

        if (InteractDialogueOrObjects.isDialogTrigger && (Input.GetKeyDown(KeyCode.E) || (Time.timeScale == 0 && Input.anyKeyDown)) /*&& !PauseMenu.GameIsPaused*/)
        {
            GeneralDialogue();
        }
    }

    public void GeneralDialogue()
    {
        if (!didDialogueStart)
        {
            StartDialogue();

        }
        else if (dialogueText.text == dialogues[dialogIndex].dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[dialogIndex].dialogueLines[lineIndex];
        }
    }

    void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
        finishDialog = false;
    }

    void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogues[dialogIndex].dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            finishDialog = true;
            Time.timeScale = 1f;
        }
    }

    IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogues[dialogIndex].dialogueLines[lineIndex])
        {
            dialogueText.text += ch;

            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    public bool FinishDialogue()
    {

        return finishDialog;
    }

    public void PutDialogue(NonDialogue[] value)
    {
        dialogues = value;
        //dialogueLines = value;
    }

    public GameObject GetdialoguePanel()
    {
        return dialoguePanel;
    }

    public void PutTextOnView(String value)
    {
        dialogueText.text = value;
    }
}
