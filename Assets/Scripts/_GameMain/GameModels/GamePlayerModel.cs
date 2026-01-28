public struct GamePlayerModel
{
    public readonly PlayerModel player;
    public int points;

    public GamePlayerModel (PlayerModel player)
    {
        this.player = player;
        points = 0;
    }
}