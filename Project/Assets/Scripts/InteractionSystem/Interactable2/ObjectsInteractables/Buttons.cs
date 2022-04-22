using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        PauseMenu.pause = false;

    }
}
