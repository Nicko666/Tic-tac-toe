using System;

[Serializable]
public class ProgressData
{
    public int fieldID;
    public int levelsID;
    public int boardID;
    public PlayerData[] playersData = new PlayerData[2]
    {
        new PlayerData(0, 0, 0.0f, 0.8f),
        new PlayerData(0, 1, 0.7f, 0.8f),
    };
}

[Serializable]
public struct PlayerData
{
    public int logicID;
    public int markID;
    public float hue;
    public float saturation;

    public PlayerData(int logicID, int markID, float hue, float saturation)
    {
        this.logicID = logicID;
        this.markID = markID;
        this.hue = hue;
        this.saturation = saturation;
    }
}