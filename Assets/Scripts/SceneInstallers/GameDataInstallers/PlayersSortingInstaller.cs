public class PlayersSortingInstaller : PersistentGameDataInstaller
{
    PropertyModel<PlayersSorting> _playersSorting;


    public override void InstallBindings()
    {
        Container.Bind<PropertyModel<PlayersSorting>>().FromInstance(_playersSorting).AsSingle().NonLazy();
    }

    public override void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator)
    {
        _playersSorting = new(database.playersSortingDatabase.GetItemByIndex(gameData.playersSortingIndex));
    }

    public override void Save(GameData gameData, DatabaseInstaller database)
    {
        gameData.playersSortingIndex = database.playersSortingDatabase.GetIndexByItem(_playersSorting.property.Value);
    }


}
