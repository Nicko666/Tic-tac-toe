using System.Collections.ObjectModel;
using UnityEngine;

public class WinnerFirstSortingCommand : IPlayersSortingHeandler
{
    public void Sort(ReactiveCollection<PlayerViewModel> players)
    {
        Debug.Log($"{players[0].Name} is first. no need for change");

    }


}