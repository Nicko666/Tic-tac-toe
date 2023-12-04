public class MaxPointsModelInstaller : PersistentGameDataInstaller
{
    PropertyModel<MaxPoints> _maxPointsModel;


    public override void InstallBindings()
    {
        Container.Bind<PropertyModel<MaxPoints>>().FromInstance(_maxPointsModel).AsSingle().NonLazy();
    }

    public override void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator)
    {
        _maxPointsModel = new(database.maxPointsDatabase.GetItemByIndex(gameData.maxPointsIndex));
    }

    public override void Save(GameData gameData, DatabaseInstaller database)
    {
        gameData.maxPointsIndex = database.maxPointsDatabase.GetIndexByItem(_maxPointsModel.property.Value);
    }


}
