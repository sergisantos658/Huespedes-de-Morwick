using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
