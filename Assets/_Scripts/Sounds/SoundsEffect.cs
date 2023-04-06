using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Zenject;

public class SoundsEffect : MonoBehaviour
{
    public static SoundsEffect Instance { get; private set; }

    [Space]
    [SerializeField] private SoundEffectSO _soundEffectSO;
    [Space]
    [Range(0, 1f)][SerializeField] private float _volume = 0.5f;

    private PlayerHealth _playerHealth;

    [Inject]
    public void Constructor(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerHealth.OnPlayerTakeDamage += PlayerHealth_OnPlayerTakeDamage;
    }

    public void PlaySoundThrow(AnimationEvents eventsArray)
    {
        PlaySound(_soundEffectSO.SoudnsThrow, eventsArray.gameObject.transform.position);
    }

    public void PlaySoundScreamSquirrel(AnimationEvents eventsArray)
    {
        float pitch = 1.5f;
        PlaySound(_soundEffectSO.SoundsScreamSquirrel, eventsArray.gameObject.transform.position, pitch);
    }

    public void PlaySoundTakeDamage(PlayerHealth playerHealth)
    {
        PlaySound(_soundEffectSO.SoundsTakeDamage, playerHealth.gameObject.transform.position);
    }

    public void PlaySoundHitEnemy(EnemyHealth enemyHealth)
    {
        PlaySound(_soundEffectSO.SoundsDamageEnemy, enemyHealth.gameObject.transform.position);
    }

    public void PlaySoundFxDieEnemy(EnemyHealth enemyHealth)
    {
        PlaySound(_soundEffectSO.SoundsDieFx, enemyHealth.gameObject.transform.position);
    }

    public void PlaySoundShoot(Gun gun)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        PlaySound(_soundEffectSO.SoundsShoot, gun.gameObject.transform.position, pitch);
    }

    public void PlaySoundTakeLoot(Loot loot)
    {
        PlaySound(_soundEffectSO.SoundsTakeHealth, loot.gameObject.transform.position);
    }


    private void PlaySound(AudioClip audioClip, Vector3 position, float pitch = 1f)
    {
        string objectName = "One shot audio";
        GameObject gameObject = new(objectName);
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = _volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        Destroy(gameObject, audioClip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }

    private void PlaySound(List<AudioClip> audioClips, Vector3 positionPlay, float pitch = 1f)
    {
        int index = Random.Range(0, audioClips.Count - 1);
        PlaySound(audioClips[index], positionPlay, pitch);
    }

    private void PlayerHealth_OnPlayerTakeDamage(object sender, System.EventArgs e)
    {
        PlayerHealth playerHealth = sender as PlayerHealth;
        PlaySoundTakeDamage(playerHealth);
    }
}
