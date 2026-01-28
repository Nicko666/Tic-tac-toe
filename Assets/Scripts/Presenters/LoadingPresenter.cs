using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPresenter : MonoBehaviour
{
    [SerializeField] private float _loadingDelay;
    [SerializeField] private Animator _animator;
    private static readonly int AnimatorIsLoadingBool = Animator.StringToHash("IsLoading");

    public void OutputLoading(bool isLoading) =>
        _animator.SetBool(AnimatorIsLoadingBool, isLoading);

    public void OutputGameScene(SceneModel sceneModel)
    {
        StartCoroutine(LoadSceneAsync(sceneModel));
        IEnumerator LoadSceneAsync(SceneModel sceneModel)
        {
            _animator.SetBool(AnimatorIsLoadingBool, true);
            yield return new WaitForSeconds(_loadingDelay);
            yield return SceneManager.LoadSceneAsync(sceneModel.ToString());

            _animator.SetBool(AnimatorIsLoadingBool, false);
            yield return new WaitForSeconds(_loadingDelay);
        }
    }
}