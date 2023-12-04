using System.Collections.Generic;

public class GameData
{
    public List<PlayerData> playersData;

    public int fieldIndex;

    public int playersSortingIndex;

    public int maxPointsIndex;

    public List<RecordData> recordsData;


    public GameData()
    {
        //PlayerData xPlayer = new(0.6f, 0, "Player 1", 0, 0);
        //PlayerData oPlayer = new(0.0f, 1, "Player 2", 1, 0);

        playersData = new() { new(0.6f, 0, "Player 1", 0, 0), new(0.0f, 1, "Player 2", 1, 0) };

        fieldIndex = 0;

        playersSortingIndex = 0;

        maxPointsIndex = 1;

        recordsData = new();

        //recordsData.Add( new(playersData, 0, 0, 0) );

    }


}