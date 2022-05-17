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

        int count = SaveItemsSytem.Load<int>("/items.count");
        if(count >= 0)
        {
            for (int i = 0; i < count; i++)
            {
                ItemsData data = SaveItemsSytem.Load<ItemsData>("/items" + i);
                Items item = Resources.Load<Items>(data.scriptedItemName);
                item.pickUp = false;
                SaveItemsSytem.DeleteFile("/items" + i);
            }

            SaveItemsSytem.DeleteFile("/items.count");
        }

        MenuManager.pause = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadGame()
    {

        MenuManager.pause = false;
        Time.timeScale = 1;

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
