using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent OnEvent;

    public void StartEvent()
    {
        OnEvent.Invoke();
    }

    public void PlaySoundScreamSquirrel()
    {
        SoundsEffect.Instance.PlaySoundScreamSquirrel(this);
    }

    public void PlaySoundThrow()
    {
        SoundsEffect.Instance.PlaySoundThrow(this);
    }
}
