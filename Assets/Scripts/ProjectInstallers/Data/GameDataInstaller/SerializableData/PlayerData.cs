[System.Serializable]
public class PlayerData
{
    public float hue;
    public int markID;
    public string name;
    public int playerBehaviourID;
    public int points;


    public PlayerData(float hue, int markID, string name, int playerBehaviourID, int points)
    {
        this.hue = hue; this.markID = markID; this.name = name; this.playerBehaviourID = playerBehaviourID; this.points = points;
    }


}