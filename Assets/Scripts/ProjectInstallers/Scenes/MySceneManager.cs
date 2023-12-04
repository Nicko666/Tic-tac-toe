using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    int _menuSceneNumber = 0;
    int _gameSceneNumber = 1;


    public Action onSceneChange;


    public void MenuScene()
    {
        onSceneChange?.Invoke();
        LoadScene(_menuSceneNumber);
    }

    public void GameScene()
    {
        onSceneChange?.Invoke();
        LoadScene(_gameSceneNumber);
    }

    void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }


}
