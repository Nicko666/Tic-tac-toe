using System;

[Serializable]
public class ProgressData
{
    public int fieldID;
    public int levelsID;
    public int boardID;
    public PlayerData[] playersData = new PlayerData[1]
    {
        new PlayerData()
    };
}

[Serializable]
public struct PlayerData
{
    public int logicID;
    public int markID;
    public float hue;
    public float saturation;
}