using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public ChangeScene changeToMain;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("WaitToEnd",45);
    }

    // Update is called once per frame
    public void WaitToEnd()
    {
        changeToMain.SceneLoad();
    }
}
