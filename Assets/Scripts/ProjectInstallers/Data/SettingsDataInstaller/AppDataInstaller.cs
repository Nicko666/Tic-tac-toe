using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppDataInstaller : MonoInstaller
{
    [Inject] DatabaseInstaller _databaseInstaller;

    AppData _appData = null;
    string _fileName = "SettingsData";
    DataHandler<AppData> _dataHandler = new();
    List<PersistentAppDataInstaller> _appDataInstallers;


    [Inject]
    void SceneChangeSubscibtion(MySceneManager sceneManager)
    {
        sceneManager.onSceneChange += SaveData;
    }

    public override void InstallBindings()
    {
        if (_appData == null)
        {
            LoadData();
        }
        _appDataInstallers = new List<PersistentAppDataInstaller>();

        Container.Bind<AppData>().FromInstance(_appData).AsSingle().NonLazy();
        Container.Bind<List<PersistentAppDataInstaller>>().FromInstance(_appDataInstallers).AsSingle().NonLazy();

    }

    void LoadData()
    {
        _appData = _dataHandler.Load(_fileName);

        if (_appData == null)
        {
            Debug.Log("No settings data was found. Initializing data to defaults");
            _appData = new AppData();
        }

    }

    void SaveData()
    {
        foreach(var appData in _appDataInstallers)
            appData.Save(_appData, _databaseInstaller);

        _dataHandler.Save(_appData, _fileName);

    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
            Debug.Log("AppData lost focus");
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }


}
