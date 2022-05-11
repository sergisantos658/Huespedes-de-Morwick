using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetPuzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectPlanet;
    public PlayerController player;
    public static bool Planets = false;
    public static bool correct = false;
    [SerializeField] private Texture2D normalCursor;

    [SerializeField]
    private DialogueObject[] dialoguePlanets;

    public List<GameObject> listaPlanet = new List<GameObject>();


    public void activatepuzzle()
    {
        objectPlanet.SetActive(true);
        Vector2 normalCursorHotspot = new Vector2(20, 10);
        Cursor.SetCursor(normalCursor, normalCursorHotspot, CursorMode.ForceSoftware);
        Planets = true;
        Time.timeScale = 0;

    }
    public void touchPlanet(int num)
    {
        objectPlanet.SetActive(false);
        Planets = false;
        Time.timeScale = 1;
        if (listaPlanet[num] == listaPlanet[num])
        {
            player.DialogueUI.ShowDialogue(dialoguePlanets[num]);
        }
        if (listaPlanet[num] == listaPlanet[0])
        {
            correct = true;
        }
    }
}
