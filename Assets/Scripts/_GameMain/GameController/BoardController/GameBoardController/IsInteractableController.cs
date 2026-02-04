using System;

public class IsInteractableController
{
    private bool _isInteractable;

    internal bool Get() =>
        _isInteractable;

    internal void Set(TileModel[,] tiles, PlayerModel winner)
    {
        bool hasEmptyTile = false;
        
        foreach (TileModel tile in tiles)
            if (tile.player == null)
            {
                hasEmptyTile = true;
                break;
            }

        _isInteractable = hasEmptyTile && winner == null;
    }
}
