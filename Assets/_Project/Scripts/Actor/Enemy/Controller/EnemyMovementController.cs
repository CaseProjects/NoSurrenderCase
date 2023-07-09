using EnemyBehavior;
using MainHandlers;
using PlayerBehaviors;
using UnityEngine;
using Zenject;

public class EnemyMovementController : IInitializable, ITickable
{
    #region INJECT

    private readonly EnemyModel _enemyModel;
    private readonly BaseObservables _baseObservables;
    private readonly GameStateManager _gameStateManager;
    private readonly AnimationHandler _animationHandler;

    public EnemyMovementController(EnemyModel enemyModel, BaseObservables baseObservables,
        GameStateManager gameStateManager, AnimationHandler animationHandler)
    {
        _enemyModel = enemyModel;
        _baseObservables = baseObservables;
        _gameStateManager = gameStateManager;
        _animationHandler = animationHandler;
    }

    #endregion

    private readonly Collider[] _overlapColliders = new Collider[10];
    private Transform _activeTarget;

    public void Initialize()
    {
        _gameStateManager.GameStateReactiveProperty.Subscribe(state =>
        {
            if (state == GameStateManager.GameStates.InGameState)
                _baseObservables.RunnerState.Value = BaseObservables.RunnerStates.Walking;
        });

        _baseObservables.RunnerState.Subscribe(runnerState =>
        {
            SetActiveNavMeshAgent(runnerState == BaseObservables.RunnerStates.Walking);
            UpdateAnimations(runnerState);
        });
    }

    private void SetActiveNavMeshAgent(bool isActive)
    {
        _enemyModel.NavMeshAgent.enabled = isActive;
    }

    private void UpdateAnimations(BaseObservables.RunnerStates enemyStates)
    {
        switch (enemyStates)
        {
            case BaseObservables.RunnerStates.Walking:
                _animationHandler.Play(Constants.AnimationState.RUN);
                break;
            case BaseObservables.RunnerStates.Hit:
                _animationHandler.Play(Constants.AnimationState.HIT);
                break;
        }
    }

    public void Tick()
    {
        if (_baseObservables.RunnerState.Value != BaseObservables.RunnerStates.Walking) return;

        if (_activeTarget)
        {
            if (IfTargetClose())
                _enemyModel.NavMeshAgent.SetDestination(_activeTarget.position);
            else
                FindNewTarget();
        }
        else
            FindNewTarget();
    }

    private bool IfTargetClose()
    {
        return _activeTarget.gameObject.activeInHierarchy &&
               Vector3.Distance(_enemyModel.GO.transform.position, _activeTarget.position) < 30;
    }

    private void FindNewTarget()
    {
        var count = Physics.OverlapSphereNonAlloc(_enemyModel.GO.transform.position, 100, _overlapColliders, 1 << 6);
        if (count <= 1) _activeTarget = null;

        var tempTarget = _overlapColliders[Random.Range(0, count)].transform;
        while (tempTarget == _enemyModel.GO.transform)
        {
            tempTarget = _overlapColliders[Random.Range(0, count)].transform;
        }

        _activeTarget = tempTarget;
    }
}