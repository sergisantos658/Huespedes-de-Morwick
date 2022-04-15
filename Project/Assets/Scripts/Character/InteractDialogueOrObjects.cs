using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogueOrObjects : MonoBehaviour
{
    [SerializeField] private GameObject cameraToInteract;
    [SerializeField] private GameObject iconDialog;
    public static bool isDialogTrigger;

    private bool isInteractTriggerActive;
    private bool onlyOnce = false;

    private void Awake()
    {
        iconDialog.SetActive(false);
        isInteractTriggerActive = false;
    }

    private void Update()
    {
        if(isInteractTriggerActive)
            iconDialog.transform.LookAt(cameraToInteract.transform);

        iconDialog.SetActive(isInteractTriggerActive);
    }

    public void StopInteract()
    {
        if (!onlyOnce)
        {
            isInteractTriggerActive = false;

            onlyOnce = true;
        }
        
    }

    public void Interacting()
    {
        isInteractTriggerActive = true;

        onlyOnce = false;
    }
}
