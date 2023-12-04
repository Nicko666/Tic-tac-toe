using System.Collections.ObjectModel;

public class PlayersQueueModel
{
    public ReactiveCollection<PlayerModel> playerModels = new();

    public ReactiveProperty<int> minCount = new();

    public ReactiveProperty<int> maxCount = new();


    public PlayersQueueModel(ObservableCollection<PlayerModel> playerModels, int minCount, int maxCount)
    {
        this.playerModels.Value = playerModels;
        this.minCount.Value = minCount;
        this.maxCount.Value = maxCount;

    }


}
