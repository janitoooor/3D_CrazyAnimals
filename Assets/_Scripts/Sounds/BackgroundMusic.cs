using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [Space]
    [Range(0, 1f)][SerializeField] private float _volume;

    #region Mono
    private void Awake()
    {
        SetOptionsAudioSource();
    }

    #endregion

    private void SetOptionsAudioSource()
    {
        _audioSource.playOnAwake = false;
        _audioSource.volume = _volume;
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void EnabledAudioSource(bool isEnabled)
    {
        if (isEnabled)
            _audioSource.UnPause();
        else
            _audioSource.Pause();
    }
}
