using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundEffectSO")]
public class SoundEffectSO : ScriptableObject
{
    [SerializeField] private List<AudioClip> _soundsShoot;
    public List<AudioClip> SoundsShoot => _soundsShoot;
    [SerializeField] private List<AudioClip> _soundsTakeHealth;
    public List<AudioClip> SoundsTakeHealth => _soundsTakeHealth;
    [SerializeField] private List<AudioClip> _soundsTakeDamage;
    public List<AudioClip> SoundsTakeDamage => _soundsTakeDamage;
    [SerializeField] private List<AudioClip> _soundsDamageEnemy;
    public List<AudioClip> SoundsDamageEnemy => _soundsDamageEnemy;
    [SerializeField] private List<AudioClip> _soundsScreamSquirrel;
    public List<AudioClip> SoundsScreamSquirrel => _soundsScreamSquirrel;
    [SerializeField] private List<AudioClip> _soundsThrow;
    public List<AudioClip> SoudnsThrow => _soundsThrow;
    [SerializeField] private List<AudioClip> _soundsDieFx;
    public List<AudioClip> SoundsDieFx => _soundsDieFx;

}
