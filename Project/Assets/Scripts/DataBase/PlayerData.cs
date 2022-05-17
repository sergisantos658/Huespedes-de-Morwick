using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public bool tutorial;
    public int puzzle1;
    public int puzzle2;
    public int puzzle3_1;
    public int puzzle3_2;

    public PlayerData(PlayerCheckPoint player)
    {
        level = player.level;
        tutorial = player.tutorial;
        puzzle1 = player.puzzle1;
        puzzle2 = player.puzzle2;
        puzzle3_1 = player.puzzle3_1;
        puzzle3_2 = player.puzzle3_2;
    }
}
