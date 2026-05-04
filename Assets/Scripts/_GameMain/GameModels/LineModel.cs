public struct LineModel
{
    public DirectionType direction;
    public (int x, int y)[] tilesIndex;
    public PlayerModel winner;
    public bool hasWinTiles;

    public enum DirectionType
    {
        Vertical,
        Horizontal,
        Up,
        Down,
    }
}
