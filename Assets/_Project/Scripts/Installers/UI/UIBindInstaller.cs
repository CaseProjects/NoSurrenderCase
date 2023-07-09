namespace Installers.UI
{
    using MainHandlers.UI;
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Zenject;

    public class UIBindInstaller : MonoInstaller
    {
        [SerializeField, HideLabel, LabelText("UI SETTINGS")]
        private UISettings _settings;

        public override void InstallBindings()
        {
            InstallUIBindings();
            InstallSettingsInstances();
        }


        private void InstallUIBindings()
        {
            Container.BindInterfacesTo<UIManager>().AsSingle();
            Container.BindInterfacesTo<WarmupController>().AsSingle();
            Container.BindInterfacesTo<ArenaController>().AsSingle();
            Container.BindInterfacesTo<PlayerScoreController>().AsSingle();
        }

        private void InstallSettingsInstances()
        {
            Container.BindInstance(_settings.UIBase).AsSingle();
            Container.BindInstance(_settings.WarmupController).AsSingle();
            Container.BindInstance(_settings.ArenaController).AsSingle();
            Container.BindInstance(_settings.PlayerScoreController).AsSingle();
        }

        [Serializable]
        private class UISettings
        {
            [TabGroup("Base UI Settings")] [HideLabel]
            public UIManager.BaseSettings UIBase;

            public WarmupController.Settings WarmupController;
            public ArenaController.Settings ArenaController;
            public PlayerScoreController.Settings PlayerScoreController;
        }
    }
}