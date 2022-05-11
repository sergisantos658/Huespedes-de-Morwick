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
    [SerializeField] private Texture2D PointCursor;

    private Vector2 interactCursorHotspot;
    private Vector2 normalCursorHotspot;
    public void Start()
    {
        normalCursorHotspot = new Vector2(10, 10);
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
                //Cursor.visible = false;
                Cursor.SetCursor(PointCursor, normalCursorHotspot, CursorMode.ForceSoftware);
            }
            else 
            {
                //Cursor.visible = true;
                player.SetActive(true);
                ghost.SetActive(true);
                Len.SetActive(false); 
            }
        }
    }
}
