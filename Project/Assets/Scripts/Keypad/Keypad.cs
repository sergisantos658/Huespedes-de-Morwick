using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Keypad : MonoBehaviour
{
    public int maxCaracters = 4;
    public string password = "1234";
    private string userInput = "";
    public string simbol = "#";

    private float temp = 0.0f;
    public float tempBlink = 1.0f;
    bool isBlink = false;

    public TextMeshPro text;

    public AudioClip clickSound;

    public InteractKeypad interactKeypad;

    AudioSource audioSource;

    public UnityEvent OnEntryAllowed;

    void Start()
    {
        temp = 0.0f;
        userInput = "";
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        temp -= Time.deltaTime;

        if (temp < 0.0f)
        {
            isBlink = !isBlink;
            temp = tempBlink;
            UpdateText();
        }
    }

    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);

        if(userInput.Length <= maxCaracters - 1)
        {
            userInput += number;
            UpdateText();
        }
    }

    public void clear()
    {
        userInput = "";
        UpdateText();
    }

    public void enter()
    {
        if(userInput == password)
        {
            OnEntryAllowed.Invoke();
            userInput = "";
            UpdateText();
            interactKeypad.Return();
        }

        else
        {
            userInput = "";
            UpdateText();
        }
    }

    void UpdateText()
    {
        //Debug.Log(userInput);
        text.text = userInput + ((userInput.Length <= maxCaracters - 1) && isBlink? simbol : "");
    }
}
