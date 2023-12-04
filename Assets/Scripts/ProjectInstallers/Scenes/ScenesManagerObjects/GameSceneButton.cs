using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GameSceneButton : MonoBehaviour, IPointerClickHandler
{
    [Inject] MySceneManager sceneManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        sceneManager.GameScene();
    }


}
