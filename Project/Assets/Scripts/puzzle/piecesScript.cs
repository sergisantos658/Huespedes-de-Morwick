using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piecesScript : MonoBehaviour
{
    Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    public DragAndDrop_ Drag;
    public ChangeScene change;
    public static bool puzzleCompleted = false;
    private static int pieceCorrect;


 
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(-137f,-130f),Random.Range(75f,68f));
    }

    void Update()
    {
        if (Vector3.Distance(transform.position,RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if (!InRightPosition)
                {
                    colocePuzzle();
                }
            }
        }
        if(pieceCorrect == Drag.numPieces)
        {
            puzzleCompleted = true;
            Drag.finshPuzzle();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            colocePuzzle();
        }
    }
    public void colocePuzzle()
    {
        transform.position = RightPosition;
        InRightPosition = true;
        pieceCorrect++;
        GetComponent<SortingGroup>().sortingOrder = 0;
    }

}
