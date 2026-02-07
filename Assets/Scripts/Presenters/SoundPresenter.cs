using UnityEngine;

public class SoundPresenter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _acceptClip, _rejectClip, _moveClip;

    public void OutputSound(SoundModel sound)
    {
        AudioClip clip = sound switch 
        {
            SoundModel.Move => _moveClip,
            SoundModel.Reject => _rejectClip, 
            _ => _acceptClip,
        };

        _audioSource.PlayOneShot(clip);
    }

    public void OutputSettings(float volume)
    {
        _audioSource.volume = volume;
    }
}

