using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalcredits : MonoBehaviour
{
    // Start is called before the first frame update

    public ChangeScene change;
    // Update is called once per frame
    void Update()
    {
        Invoke("changeCredits", 27f);
    }
    public void changeCredits()
    {
        change.SceneLoad();
    }
}
