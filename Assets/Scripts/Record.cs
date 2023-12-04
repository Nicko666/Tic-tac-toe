using System.Collections.Generic;

public class Record
{
    public IEnumerable<PlayerModel> playersModels;
    public Field field;
    public PlayersSorting playersSorting;
    public MaxPoints maxPoints;


    public Record(IEnumerable<PlayerModel> playersModels, Field field, PlayersSorting playersSorting, MaxPoints maxPoints)
    {
        this.playersModels = playersModels;
        this.field = field;
        this.playersSorting = playersSorting;
        this.maxPoints = maxPoints;

    }


}