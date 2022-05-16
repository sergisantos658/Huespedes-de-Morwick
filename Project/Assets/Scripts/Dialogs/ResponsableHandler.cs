using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResponsableHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private ResponseEvent[] responseEvents;

    private List<ResponseEvent[]> responseEventsList = new List<ResponseEvent[]>();

    private List<GameObject> temporalResponseButton = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseEventsList.Add(responseEvents);

    }
    
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0; // To increase the Height of the Response box

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.gameObject.name = "OptionTemplateAndText " + responseIndex;
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickResponse(response, responseIndex));

            temporalResponseButton.Add(responseButton); // Every time we create a button, we add it to the list 

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickResponse(Response response, int responseIndex)
    {

        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in temporalResponseButton)
        {
            Destroy(button);
        }

        //if (responseEvents != null && responseIndex <= responseEvents.Length)
        //{
        //    responseEvents[responseIndex].OnPickedResponse?.Invoke();
        //}

        for (int i = 0; i < responseEventsList.Count; i++)
        {
            if (responseEventsList != null && responseIndex <= responseEventsList[i].Length)
            {
                responseEventsList[i][responseIndex].OnPickedResponse?.Invoke();
            }

        }

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }

        responseEventsList.Clear();
    }
}
