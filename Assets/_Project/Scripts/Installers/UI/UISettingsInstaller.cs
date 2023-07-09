using System;
using UnityEngine;
using Zenject;

public class UISettingsInstaller : MonoInstaller
{
    [SerializeField] private PlayerSettings _playerSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerSettings.PlayerMoveHandler).AsSingle();
    }

    [Serializable]
    private class PlayerSettings
    {
        public PlayerMoveHandler.UISettings PlayerMoveHandler;
    }
}