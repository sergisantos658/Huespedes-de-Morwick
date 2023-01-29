using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Toggle togle;

    void Start()
    {
        if (Screen.fullScreen)
        {
            togle.isOn = true;
        }
        else
        {
            togle.isOn = false;
        }
    }

    public void FullScreenActivate(bool screen)
    {
        Screen.fullScreen = screen;
    }
}
