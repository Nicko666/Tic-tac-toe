using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PersistentAppDataInstaller : MonoInstaller
{
    [SerializeField] bool _doSave;

    [Inject]
    public abstract void Load(AppData appData, DatabaseInstaller database);

    [Inject] 
    void AddToSave(List<PersistentAppDataInstaller> appDataInstallers)
    {
        if (_doSave)
            appDataInstallers.Add(this);

    }

    public abstract void Save(AppData appData, DatabaseInstaller database);


}
