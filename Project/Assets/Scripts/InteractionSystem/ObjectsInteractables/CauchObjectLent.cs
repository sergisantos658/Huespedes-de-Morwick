using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauchObjectLent : Interactable
{
    // Start is called before the first frame update
    public DialogueObject obs;
    public Vector3 Pose;
    public Quaternion Rotation;
    public GameObject player;
    public GameObject Ghost;
    public GameObject lens;
    public static bool keyS3;


    private PlayerController playerC;
    private void OnEnable()
    {
        PlayerController.WhoIsPlayerController += WhoIsPlayerController;
    }

    private void OnDisable()
    {
        PlayerController.WhoIsPlayerController -= WhoIsPlayerController;
    }

    void WhoIsPlayerController(PlayerController target)
    {
        playerC = target;
    }
    public override void Interact()
    {
        player.transform.position = Pose;
        player.transform.rotation = Rotation;
        player.SetActive(true);
        Ghost.SetActive(true);
        lens.SetActive(false);
        ActivateLen.Active = false;
        playerC.DialogueUI.ShowDialogue(obs);
        keyS3 = true;
        Cursor.visible = true;
        gameObject.SetActive(false);

    }
    public override void Observation()
    {
        
    }
}
