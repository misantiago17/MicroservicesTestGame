using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string player;
    public int points;

    public ScoreData(string player, int points)
    {
        this.player = player;
        this.points = points;
    }
}
