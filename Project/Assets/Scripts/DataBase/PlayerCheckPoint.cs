using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckPoint : MonoBehaviour
{
    
    private static PlayerCheckPoint _instance;
    public static PlayerCheckPoint Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerCheckPoint>();

            }
            return _instance;

        }
    }

    // Posicion donde empieza el personaje en la sala
    public GameObject posicionDeInicio;
    // Posicion donde aparece el personaje despues de volver de la siguiente sala
    public GameObject posicionDeRetorno;

    public int level;

    PlayerData data;

    private void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;

        data = DBManager.SelectPlayerData(this);
        if (data != null && data.level != level)
        {
            LoadGame();
        }
        else
        {
            Debug.Log("Non Data Select");
        }

    }

    private void Start()
    {
        //transform.position = new Vector3(positionX, positionY, 0);

    }



    public void CheckPointSave()
    {
        DBManager.UpdatePlayerData(this);
        LoadGame();
    }

    void LoadGame()
    {
        // cambiar de posicion al personaje según si el nivel guardado es menor o mayor que el actual usando 2 Transform's que hayas escogido
        if(data.level > level)
        {
            transform.position = posicionDeRetorno.transform.position;
        }
        else if(data.level <= level)
        {
            transform.position = posicionDeInicio.transform.position;
        }

    }
    
}
