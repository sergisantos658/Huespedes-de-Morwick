using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScene : MonoBehaviour
{
    public ChangeScene sceneChange;
    public void changeScene()
    {
        Debug.Log("Change");
        sceneChange.SceneLoad();
    }
}
