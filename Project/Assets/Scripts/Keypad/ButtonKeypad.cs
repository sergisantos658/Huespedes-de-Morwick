using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonKeypad : MonoBehaviour
{
    public int keyNum;

    public UnityEvent KeyClicked;

    void OnMouseDown()
    {
        Debug.Log($"click {keyNum}");
        KeyClicked.Invoke();
    }
}
