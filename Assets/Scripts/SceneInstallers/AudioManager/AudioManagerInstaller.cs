using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManagerInstaller : MonoInstaller
{
    [SerializeField] AudioSource audioSource;
    
    AudioClip audioClip0;
    AudioClip audioClip1;


    public override void InstallBindings()
    {
        Container.Bind<AudioSource>().FromInstance(audioSource).AsSingle().NonLazy();
        //AudioSource.PlayClipAtPoint(audioClip1, transform.position);
    }



}
