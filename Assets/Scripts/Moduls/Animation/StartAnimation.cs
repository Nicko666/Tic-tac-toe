using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StartAnimation : MonoBehaviour
{
    void Start()
    {
        OnStart();
    }

    public abstract void OnStart();


}
