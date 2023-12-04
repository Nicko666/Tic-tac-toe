using UnityEngine;

public abstract class WindowAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        EnableAnimation();
    }

    private void OnDisable()
    {
        DisableAnimation();
    }

    protected abstract void EnableAnimation();

    protected abstract void DisableAnimation();


}
