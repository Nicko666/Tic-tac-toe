using Zenject;

public class RandomPlayerGeneratorInstaller : MonoInstaller
{
    [Inject] DatabaseModel<PlayerMark> _playerMarksDatabase;
    [Inject] DatabaseModel<PlayerBehaviour> _playerBehavioursDatabase;
    

    public override void InstallBindings()
    {
        RandomPlayerGenerator randomPlayerModel = new(_playerMarksDatabase, _playerBehavioursDatabase);
        Container.Bind<RandomPlayerGenerator>().AsSingle().NonLazy();

    }


}
