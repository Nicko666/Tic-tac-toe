using UnityEngine;

public class SoundPresenter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _acceptClip, _rejectClip;

    public void OutputSound(SoundModel sound)
    {
        AudioClip clip = sound switch 
        { 
            SoundModel.Accept => _acceptClip, 
            _ => _rejectClip, 
        };

        _audioSource.PlayOneShot(clip);
    }

    public void OutputSettings(float volume)
    {
        _audioSource.volume = volume;
    }
}

