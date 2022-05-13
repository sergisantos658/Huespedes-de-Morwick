using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float[] position;

    public PlayerData(PlayerCheckPoint player)
    {
        level = player.level;


        position = new float[3];

        position[0] = player.posicionDeInicio.transform.position.x;
        position[1] = player.posicionDeInicio.transform.position.y;
        position[2] = player.posicionDeInicio.transform.position.z;

    }
}
