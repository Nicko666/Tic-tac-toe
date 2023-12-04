using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MenuSceneButton : MonoBehaviour, IPointerClickHandler
{
    [Inject] MySceneManager sceneManager;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        sceneManager.MenuScene();
    }


}
