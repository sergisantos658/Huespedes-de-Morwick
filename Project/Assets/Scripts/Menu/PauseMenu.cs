using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool pause;
    public GameObject MenuPause;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause)
            {
                Time.timeScale = 0;
                MenuPause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                MenuPause.SetActive(false);
            }
                

        }
        /*else if(pause == true)
        {
            pause = false;
            Time.timeScale = 1;
        }*/
    }
}