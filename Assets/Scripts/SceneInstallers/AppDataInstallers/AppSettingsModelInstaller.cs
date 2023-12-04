using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppSettingsModelInstaller : PersistentAppDataInstaller
{
    //[Inject] DatabaseModel<Locals> _localsDatabase;
    //[Inject] DatabaseModel<Theme> _themeDatabase;

    AppSettingsModel _appSettingsModel;


    public override void InstallBindings()
    {
        AppSettingsViewModel appSettingsViewModel = new(_appSettingsModel);
        Container.Bind<AppSettingsModel>().FromInstance(_appSettingsModel).AsSingle().NonLazy();

    }

    public override void Load(AppData appData, DatabaseInstaller database)
    {
        Locals locals = database.localsDatabase.GetItemByIndex(appData.localIndex);
        Theme theme = database.themeDatabase.GetItemByIndex(appData.themeIndex);
        float volume = appData.volume;

        _appSettingsModel = new(locals, theme, volume);
    }

    public override void Save(AppData appData, DatabaseInstaller database)
    {
        appData.localIndex = database.localsDatabase.GetIndexByItem(_appSettingsModel.locals.Value);
        appData.themeIndex = database.themeDatabase.GetIndexByItem(_appSettingsModel.theme.Value);
        appData.volume = _appSettingsModel.volume.Value;
    }


}
