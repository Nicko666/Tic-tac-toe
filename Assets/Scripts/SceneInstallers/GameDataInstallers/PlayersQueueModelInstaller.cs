using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayersQueueModelInstaller : PersistentGameDataInstaller
{
    [SerializeField] int _minPlayers = 2;
    [SerializeField] int _maxPlayers = 4;

    PlayersQueueModel _playersQueueModel;


    public override void InstallBindings()
    {
        Container.Bind<PlayersQueueModel>().FromInstance(_playersQueueModel).AsSingle().NonLazy();
        
    }

    public override void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator)
    {
        var players = new ObservableCollection<PlayerModel>();

        foreach (PlayerData playerData in gameData.playersData)
        {
            float hue = playerData.hue;
            PlayerMark mark = database.playerMarksDatabase.GetItemByIndex(playerData.markID);
            string name = playerData.name;
            PlayerBehaviour playerBehaviour = database.playerBehavioursDatabase.GetItemByIndex(playerData.playerBehaviourID);
            int points = 0;

            players.Add(new PlayerModel(hue, mark, name, playerBehaviour, points));
        }

        _playersQueueModel = new(players, _minPlayers, _maxPlayers);

        while (_minPlayers > _playersQueueModel.playerModels.Value.Count)
            _playersQueueModel.playerModels.Add(randomPlayerGenerator.PlayerModel);
        while (_maxPlayers < _playersQueueModel.playerModels.Value.Count)
            _playersQueueModel.playerModels.Remove(_playersQueueModel.playerModels.Value.Last());

    }

    public override void Save(GameData gameData, DatabaseInstaller database)
    {
        gameData.playersData.Clear();

        foreach (var player in _playersQueueModel.playerModels.Value)
        {
            float hue = player.Hue.Value;
            int markID = database.playerMarksDatabase.Items.IndexOf(player.Mark.Value);
            string name = player.Name.Value;
            int playerBehaviourID = database.playerBehavioursDatabase.Items.IndexOf(player.Behaviour.Value);

            gameData.playersData.Add(new PlayerData(hue, markID, name, playerBehaviourID, 0));
        }

    }


}
