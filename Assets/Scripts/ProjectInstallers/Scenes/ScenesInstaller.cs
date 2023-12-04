using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ScenesInstaller : MonoInstaller
{
    [SerializeField] MySceneManager _sceneManager;

    public override void InstallBindings()
    {
        Container.Bind<MySceneManager>().FromInstance(_sceneManager).AsSingle().NonLazy();

    }


}
