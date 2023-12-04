using System.Collections.Generic;

[System.Serializable]
public class RecordData
{
    public List<PlayerData> playersDatas;

    public int fieldIndex;

    public int playersSortingIndex;

    public int maxPointsIndex;

    public RecordData(List<PlayerData> playersDatas, int fieldIndex, int playersSortingIndex, int maxPointsIndex)
    {
        this.playersDatas = playersDatas;

        this.fieldIndex = fieldIndex;

        this.playersSortingIndex = playersSortingIndex;

        this.maxPointsIndex = maxPointsIndex;

    }


}