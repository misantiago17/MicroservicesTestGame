using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerData
{
    public string player;
    public int points;

    public PlayerData(string player, int points)
    {
        this.player = player;
        this.points = points;
    }
}

[System.Serializable]
public class PlayerList
{
    public PlayerData[] players;  
}

