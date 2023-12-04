using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameDataInstaller : MonoInstaller
{
    [Inject] DatabaseInstaller _databaseInstaller;
    GameData _gameData = null;
    string _fileName = "GameData";
    DataHandler<GameData> _dataHandler = new();
    List<PersistentGameDataInstaller> _persistentInstallers; 


    [Inject] void SceneChangeSubscibtion (MySceneManager sceneManager)
    {
        sceneManager.onSceneChange += SaveData;
    }

    public override void InstallBindings()
    {
        if (_gameData == null)
            LoadData();
        
        if (_persistentInstallers == null)
            _persistentInstallers = new List<PersistentGameDataInstaller>();
        else
            _persistentInstallers.Clear();

        Container.Bind<GameData>().FromInstance(_gameData).AsSingle().NonLazy();
        Container.Bind<List<PersistentGameDataInstaller>>().FromInstance(_persistentInstallers).AsSingle().NonLazy();
    
    }

    void LoadData()
    {
        _gameData = _dataHandler.Load(_fileName);

        if (_gameData == null)
        {
            Debug.Log("No game data was found. Initializing data to defaults");
            _gameData = new();
        }

    }

    void SaveData()
    {
        foreach (var persistent in _persistentInstallers)
            persistent.Save(_gameData, _databaseInstaller);

        _dataHandler.Save(_gameData, _fileName);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
            Debug.Log("GameData lost focus");
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }


}
