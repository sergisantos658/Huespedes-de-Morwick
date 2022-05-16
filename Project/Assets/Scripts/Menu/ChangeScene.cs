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
        MenuManager.pause = false;
        Time.timeScale = 1;

        if(PlayerCheckPoint.Instance != null)
        {
            PlayerCheckPoint.Instance.CheckPointSave();
        }

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);

    }

    public void NewGame()
    {
        DBManager.CreateDB();
        DBManager.DeletePlayerData();

        DBManager.InsertPlayerData();

        SaveItemsSytem.DeleteFile("/items");

        MenuManager.pause = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        int levelState = DBManager.GetLevelState();
        if (levelState != -1)
        {
            SceneManager.LoadScene(levelState, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
