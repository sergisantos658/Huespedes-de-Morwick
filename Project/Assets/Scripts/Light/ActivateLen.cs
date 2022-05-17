using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject ghost;
    public GameObject Len;
    public static bool Active = false;
    public bool lenActive;
    private PlayerController playerC;
    ControlObjects controlObjects;
    public Items item;
    public void Start()
    {
        playerC = PlayerController.currentPlayer;
        controlObjects = playerC.GetComponent<ControlObjects>();

    }

    public void ActiveLen()
    {
        lenActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(lenActive && Input.GetKeyDown(KeyCode.E))
        {
            Active = !Active;
            if(Active == true)
            {
                player.SetActive(false);
                ghost.SetActive(false);
                Len.SetActive(true);
                Cursor.visible = false;
            }
            else 
            {
                Cursor.visible = true;
                player.SetActive(true);
                ghost.SetActive(true);
                Len.SetActive(false); 
            }
        }
    }
}
