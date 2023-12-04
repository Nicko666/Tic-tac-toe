using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PersistentGameDataInstaller : MonoInstaller
{
    [SerializeField] bool _doSave;


    [Inject]
    public abstract void Load(GameData gameData, DatabaseInstaller database, RandomPlayerGenerator randomPlayerGenerator);

    [Inject]
    void AddToSave(List<PersistentGameDataInstaller> gameDataInstallers)
    {
        if (_doSave)
            gameDataInstallers.Add(this);
    }

    public abstract void Save(GameData gameData, DatabaseInstaller database);


}
