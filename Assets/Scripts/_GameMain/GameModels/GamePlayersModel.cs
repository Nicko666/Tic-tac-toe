public struct GamePlayersModel
{
    public GamePlayerModel[] gamePlayers;
    public PlayerModel winner;
    public int winPoitns;

    public GamePlayersModel(GamePlayerModel[] gamePlayers)
    {
        this.gamePlayers = gamePlayers;
        winner = null;
        winPoitns = 0;
    }
}
