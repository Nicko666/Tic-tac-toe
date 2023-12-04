using UnityEngine;
using Zenject;

public class DatabaseInstaller : MonoInstaller
{
    public DatabaseModel<Theme> themeDatabase;
    public DatabaseModel<Locals> localsDatabase;
    public DatabaseModel<PlayerMark> playerMarksDatabase;
    public DatabaseModel<PlayerBehaviour> playerBehavioursDatabase;
    public DatabaseModel<Field> fieldDatabase;
    public DatabaseModel<PlayersSorting> playersSortingDatabase;
    public DatabaseModel<MaxPoints> maxPointsDatabase;

    public override void InstallBindings()
    {
        Container.Bind<DatabaseInstaller>().FromInstance(this).AsSingle().NonLazy();

        Container.Bind<DatabaseModel<Theme>>().FromInstance(themeDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<Locals>>().FromInstance(localsDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<PlayerMark>>().FromInstance(playerMarksDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<PlayerBehaviour>>().FromInstance(playerBehavioursDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<Field>>().FromInstance(fieldDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<PlayersSorting>>().FromInstance(playersSortingDatabase).AsSingle().NonLazy();
        Container.Bind<DatabaseModel<MaxPoints>>().FromInstance(maxPointsDatabase).AsSingle().NonLazy();

    }


}
