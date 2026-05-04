public class IsInteractableController
{
    private bool _isInteractable;

    internal bool Get() =>
        _isInteractable;

    internal void Set(PlayerModel winner, LineModel[] lines)
    {
        bool hasWinTile = false;

        foreach (var line in lines)
        {
            if (line.hasWinTiles)
            {
                hasWinTile = true;
                break;
            }
        }

        _isInteractable = hasWinTile && winner == null;
    }
}