using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickSound : MonoBehaviour
{
    [Inject] AudioSource _audioSource;

    [SerializeField] Button _button;
    [SerializeField] AudioClip _audioClip;


    private void Start()
    {
        _button.onClick.AddListener(Play);
    }

    public void Play()
    {
        float volume = AppSettingsViewModel.volume.Value;

        if (volume > 0.01)
        {
            _audioSource.volume = volume;
            _audioSource.PlayOneShot(_audioClip);
        }
        
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Play);
    }


}
