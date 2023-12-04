using System.Collections.ObjectModel;

public class LiderFirstCommand : IPlayersSortingHeandler
{
    public void Sort(ReactiveCollection<PlayerViewModel> players)
    {
        if (players.Value.Count < 2) return;

        for (int i = 0; i < players.Value.Count; i++)
        {
            for (int j = 1; j < players.Value.Count; j++)
            {
                if (players[j].Points.Value > players[j - 1].Points.Value)
                    players.Move(j, j - 1);
            }
        }

    }


}
