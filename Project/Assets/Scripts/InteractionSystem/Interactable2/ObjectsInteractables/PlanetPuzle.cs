using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetPuzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasPlanet;
    public PlayerController player;
    public static bool Planets = false;

    [SerializeField]
    private DialogueObject dialogueCorrect;

    [SerializeField]
    private DialogueObject dialogueIncorrect;

    public List<GameObject> listaPlanet = new List<GameObject>();


    public void activatepuzzle()
    {
        canvasPlanet.SetActive(true);
        Planets = true;
        Time.timeScale = 0;

    }
    public void touchPlanet(int num)
    {
        if (listaPlanet[num] == listaPlanet[2])
        {
            player.DialogueUI.ShowDialogue(dialogueCorrect);
        }
        else
        {
            player.DialogueUI.ShowDialogue(dialogueIncorrect);
        }
    }
}
