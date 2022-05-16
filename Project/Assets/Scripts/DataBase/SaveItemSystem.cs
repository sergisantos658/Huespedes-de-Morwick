
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Follow script by Kap Koder https://www.youtube.com/channel/UCRqUSuefGa8LHrF6DOOGPPQ

public class SaveItemsSytem
{
    const string SAVES_PATH = "/saves";

    public static void Save<T>(T obj, string key)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVES_PATH;
        Directory.CreateDirectory(path);

        FileStream stream = new FileStream(path + key, FileMode.Create);

        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static T Load<T>(string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVES_PATH;

        T data = default;

        if (File.Exists(path + key))
        {
            FileStream stream = new FileStream(path + key, FileMode.Open);

            data = (T)formatter.Deserialize(stream);

            stream.Close();

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
        return data;
    }

    public static bool SaveExist(string key)
    {
        string path = Application.persistentDataPath + SAVES_PATH;
        return File.Exists(path + key);
    }

    public static void DeleteFile(string key)
    {
        string filePath = Application.persistentDataPath + SAVES_PATH;

        // check if file exists
        if (File.Exists(filePath + key))
        {
            File.Delete(filePath + key);

            RefreshEditorProjectWindow();
        }
        else
        {
            Debug.Log( "no " + filePath + key + " file exists" );
        }
    }


    static void RefreshEditorProjectWindow()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}