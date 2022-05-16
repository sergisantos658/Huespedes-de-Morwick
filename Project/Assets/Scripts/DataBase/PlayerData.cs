using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int puzzle1;
    public int puzzle2;
    public int puzzle3;

    public PlayerData(PlayerCheckPoint player)
    {
        level = player.level;
        puzzle1 = player.puzzle1;
        puzzle2 = player.puzzle2;
        puzzle3 = player.puzzle3;
    }
}
