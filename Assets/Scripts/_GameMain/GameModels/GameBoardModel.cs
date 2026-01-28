public struct GameBoardModel
{

    public readonly TileModel[,] Tiles;
    public readonly LineModel[] Lines;
    public readonly PlayerModel Winner;
    public readonly bool IsInteractable;

    public GameBoardModel(TileModel[,] tiles, LineModel[] lines, PlayerModel winner, bool isInteractable)
    {
        Tiles = tiles;
        Lines = lines;
        Winner = winner;
        IsInteractable = isInteractable;
    }
}