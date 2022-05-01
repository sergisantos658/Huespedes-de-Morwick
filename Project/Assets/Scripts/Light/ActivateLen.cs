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
    
    // Update is called once per frame
    void Update()
    {
        if(PlanetPuzle.correct == true && Input.GetKeyDown(KeyCode.E))
        {
            Active = !Active;
            if(Active == true)
            {
                player.SetActive(false);
                ghost.SetActive(false);
                Len.SetActive(true);
            }
            else 
            {
                player.SetActive(true);
                ghost.SetActive(true);
                Len.SetActive(false);
            }
        }
    }
}
