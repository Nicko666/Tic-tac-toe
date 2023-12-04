public class FieldModelInstaller : PersistentGameDataInstaller
{
    PropertyModel<Field> _model;


    public override void InstallBindings()
    {
        Container.Bind<PropertyModel<Field>>().FromInstance(_model).AsSingle().NonLazy();
    }

    public override void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator)
    {
        _model = new(database.fieldDatabase.GetItemByIndex(gameData.fieldIndex));
    }

    public override void Save(GameData gameData, DatabaseInstaller database)
    {
        gameData.fieldIndex = database.fieldDatabase.GetIndexByItem(_model.property.Value);
    }


}
