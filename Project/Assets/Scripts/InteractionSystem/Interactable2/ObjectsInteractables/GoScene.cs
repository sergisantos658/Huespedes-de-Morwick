using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScene : Interactable
{
    public ChangeScene sceneChange;
    public override void Interact()
    {
        Debug.Log("Change");
        sceneChange.SceneLoad();
    }

    public override void Observation()
    {
        
    }

   
}
