using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoactionInstaller : MonoInstaller
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerArmory _playerArmory;
    [SerializeField] private AdressablesLoader _adressablesLoader;
    [SerializeField] private GameInput _gameInput;

    public override void InstallBindings()
    {
        BindingPlayer();

        Container.BindInstance(_adressablesLoader).AsSingle();
        Container.BindInstance(_gameInput).AsSingle();
    }

    private void BindingPlayer()
    {
        Container.BindInstance(_playerMove).AsSingle();
        Container.BindInstance(_playerHealth).AsSingle();
        Container.BindInstance(_playerArmory).AsSingle();
    }
}
