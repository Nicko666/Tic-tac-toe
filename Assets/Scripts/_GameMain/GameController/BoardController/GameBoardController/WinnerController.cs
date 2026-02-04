using System;

internal class WinnerController
{
    private PlayerModel _winner;

    internal PlayerModel Get() =>
        _winner;

    internal void Set(LineModel[] lines) =>
        _winner = Array.Exists(lines, i => i.winner != null) ? _winner = Array.Find(lines, i => i.winner != null).winner : null;
}
