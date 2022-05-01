using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop_ : MonoBehaviour
{
    GameObject SelectedPiece;
    public GameObject CameraGame;
    public GameObject player;
    public int numPieces;
    public Texture2D cursor;
    private Vector2 cursorSize;
    int OIL = 1;
    private void Start()
    {
        cursorSize = new Vector2(cursor.width / 2 -2, cursor.height / 2 -6);
        Cursor.SetCursor(cursor, cursorSize, CursorMode.ForceSoftware);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            finshPuzzle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.GetComponent<piecesScript>())
            {
                if (!hit.transform.GetComponent<piecesScript>().InRightPosition)
                {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<piecesScript>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPiece != null) 
            {
                SelectedPiece.GetComponent<piecesScript>().Selected = false;
                SelectedPiece = null;
            }
        }


        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y,0);
        }
    }
    public void finshPuzzle()
    {

        CameraGame.SetActive(true);
        player.SetActive(true);
        this.gameObject.SetActive(false);
        
    }
}
