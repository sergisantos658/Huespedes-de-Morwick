using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectCharacter { Player, NPC };

[System.Serializable]
public class NonDialogue
{
    public SelectCharacter opcions;
    [TextArea(4, 6)] public string[] dialogueLines;
}

public class NonDialogueScript : MonoBehaviour
{
    public NonDialogue[] dialogues;
}
