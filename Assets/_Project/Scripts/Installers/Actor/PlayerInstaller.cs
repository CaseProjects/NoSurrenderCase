using System;
using Events;
using PlayerBehaviors;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] [TabGroup("LocalSettings")]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind(typeof(BaseModel), typeof(PlayerModel)).To<PlayerModel>().AsSingle()
                .WithArguments(_settings.Animator, _settings.Rigidbody);


            InstallPlayerHandlers();
            InstallPlayerSignals();
        }


        private void InstallPlayerHandlers()
        {
            Container.Bind<BaseObservables>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<StrengthController>().AsSingle();
            Container.BindInterfacesTo<HitController>().AsSingle();
        }

        private void InstallPlayerSignals()
        {
            Container.DeclareSignal<SignalTakeHit>().OptionalSubscriber();
        }

        [Serializable]
        [HideLabel]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public Animator Animator;
        }
    }
}