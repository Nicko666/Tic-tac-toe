internal class ProgressController
{
    private PlayersController _playersController = new();
    private RulesController _rulesController = new();

    internal void SetDatabase(ProgressDatabase database)
    {
        _rulesController.SetDatabase(database.Levels, database.Boards);
        _playersController.SetDatabase(database.MinPlayersCount, database.Logics, database.Marks);
    }

    public void GetProgress(ref ProgressModel progress, ProgressData data)
    {
        progress.players = _playersController.Get(data.playersData);
        progress.rules = _rulesController.Get(data.levelsID, data.boardID);
    }
    public void GetData(ref ProgressData data, ProgressModel progress)
    {
        _playersController.GetData(ref data.playersData, progress.players);
        _rulesController.GetData(ref data.levelsID, ref data.boardID, progress.rules);
    }
}
