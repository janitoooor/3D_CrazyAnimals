using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private ObjectPool _bulletObjectPool;
    [SerializeField] private Transform _spawnTransform;
    [Space]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootPeriod;
    [Space]
    [SerializeField] private GameObject _flash;
    [SerializeField] private ParticleSystem _shootEffect;

    private readonly float _timeToHideFlash = 0.1f;

    private float _timer;
    private bool _buttonShootIsPressed;

    private GameInput _gameInput;

    [Inject]
    private void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    #region Mono
    private void OnEnable()
    {
        _gameInput.OnMouseClickShotAction += GameInput_OnMouseClickShotAction;
        _gameInput.OnMouseUnClickShotAction += GameInput_OnMouseUnClickShotAction;
        _buttonShootIsPressed = false;

        HideFlash();
    }

    private void OnDisable()
    {
        _gameInput.OnMouseClickShotAction -= GameInput_OnMouseClickShotAction;
        _gameInput.OnMouseUnClickShotAction -= GameInput_OnMouseUnClickShotAction;
        _buttonShootIsPressed = false;
    }

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer > _shootPeriod && _buttonShootIsPressed)
        {
            _timer = 0;
            ShootBullet();
        }
    }

    #endregion

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void AddBullets(int amountBullets)
    {

    }

    private void GameInput_OnMouseUnClickShotAction(object sender, System.EventArgs e)
    {
        _buttonShootIsPressed = false;
    }

    private void GameInput_OnMouseClickShotAction(object sender, System.EventArgs e)
    {
        _buttonShootIsPressed = true;
    }

    private void HideFlash()
    {
        _flash.SetActive(false);
    }

    public virtual void ShootBullet()
    {
        Rigidbody bullet = MasterObjectPooler.Instance.GetObjectComponent<Rigidbody>(_bulletObjectPool.name);
        bullet.transform.position = _spawnTransform.position;
        bullet.velocity = _spawnTransform.forward * _bulletSpeed;

        SoundsEffect.Instance.PlaySoundShoot(this);
        _flash.SetActive(true);
        Invoke(nameof(HideFlash), _timeToHideFlash);
        _shootEffect.Play();
    }
}
