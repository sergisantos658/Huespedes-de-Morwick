using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{

    [SerializeField] private DialogueTyping[] dialogueTyping;

    [SerializeField] private Response[] responses;

    public DialogueTyping[] DialogueTyping => dialogueTyping;

    public Response[] Responses => responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;

}

[System.Serializable]
public class DialogueTyping
{
    [TextArea] public string[] dialogue;
    [SerializeField] public WhoTalk whoTalk;
}

public enum WhoTalk
{
    Player,
    NPC
}