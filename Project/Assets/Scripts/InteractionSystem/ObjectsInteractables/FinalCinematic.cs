using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCinematic : Interactable
{
    private Animator anim;
	public AudioSource normalMusic;
	public AudioSource boxMusic;

	public DialogueObject obsBox;
	public DialogueObject normalDialogue;
	private PlayerController playerC;
	public Camera cameraCinematic;
	public Camera Maincamera;
	public Items item;
	private ControlObjects inventory;
	public GameObject inventoryObject;
	public ChangeScene creditchange;

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
		playerC = PlayerController.currentPlayer;
		cameraCinematic.gameObject.SetActive(false);
		inventory = playerC.GetComponent<ControlObjects>();
		cameraCinematic = cameraCinematic.GetComponent<Camera>();
	}

	// Update is called once per frame
	public override void Interact()
	{
		
		if(!inventory.ObjectOn(item))
		{
			playerC.DialogueUI.ShowDialogue(normalDialogue);
		}
		else
		{
			normalMusic.Stop();
			boxMusic.Play();
			inventoryObject.SetActive(false);
			playerC.gameObject.SetActive(false);
			Maincamera.gameObject.SetActive(false);
			cameraCinematic.gameObject.SetActive(true);
			anim.enabled = true;
			Invoke("changeCredit", 27.0f);


		}
	}
	void changeCredit()
    {
		creditchange.SceneLoad();
    }
    public override void Observation()
	{
		playerC.DialogueUI.ShowDialogue(obsBox);
	}
}
