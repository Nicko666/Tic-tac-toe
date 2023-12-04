using System.Collections.ObjectModel;

public class WinnerLastSortingCommand : IPlayersSortingHeandler
{
    public void Sort(ReactiveCollection<PlayerViewModel> players)
    {
        players.Move(0, (players.Value.Count - 1));

    }


}