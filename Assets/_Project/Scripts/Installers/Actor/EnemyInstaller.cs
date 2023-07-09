using System;
using EnemyBehavior;
using Events;
using PlayerBehaviors;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] [TabGroup("LocalSettings")]
    private Settings _settings;


    public override void InstallBindings()
    {
        Container.Bind(typeof(BaseModel), typeof(EnemyModel)).To<EnemyModel>().AsSingle()
            .WithArguments(_settings.Animator, _settings.Rigidbody, _settings.NavMeshAgent);

        InstallEnemyHandlers();
        InstallEnemySignals();
    }

    private void InstallEnemyHandlers()
    {
        Container.Bind<BaseObservables>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnimationHandler>().AsSingle();
        Container.BindInterfacesTo<EnemyMovementController>().AsSingle();
        Container.BindInterfacesTo<StrengthController>().AsSingle();
        Container.BindInterfacesTo<HitController>().AsSingle();
    }

    private void InstallEnemySignals()
    {
        Container.DeclareSignal<SignalTakeHit>().OptionalSubscriber();
    }


    [Serializable]
    [HideLabel]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public Animator Animator;
        public NavMeshAgent NavMeshAgent;
    }
}