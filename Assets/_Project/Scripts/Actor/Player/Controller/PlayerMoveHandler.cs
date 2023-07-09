using System;
using MainHandlers;
using PlayerBehaviors;
using UnityEngine;
using Zenject;

public class PlayerMoveHandler : IInitializable, ITickable
{
    #region INJECT

    private readonly BaseModel _model;
    private readonly BaseObservables _playerObservables;
    private readonly GameStateManager _gameStateManager;
    private readonly AnimationHandler _animationHandler;

    private readonly UISettings _uıSettings;

    private PlayerMoveHandler(BaseModel model, BaseObservables playerObservables,
        AnimationHandler animationHandler, UISettings uıSettings, GameStateManager gameStateManager)
    {
        _model = model;
        _playerObservables = playerObservables;
        _animationHandler = animationHandler;
        _uıSettings = uıSettings;
        _gameStateManager = gameStateManager;
    }

    #endregion


    public void Initialize()
    {
        _gameStateManager.GameStateReactiveProperty.Subscribe(UpdatePlayerState);
        _playerObservables.RunnerState.Subscribe(playerState =>
        {
            UpdateAnimations(playerState);
            if (playerState == BaseObservables.RunnerStates.Death) _model.GO.SetActive(false);
        });
    }

    private void UpdatePlayerState(GameStateManager.GameStates state)
    {
        _playerObservables.RunnerState.Value = state switch
        {
            GameStateManager.GameStates.InGameState => BaseObservables.RunnerStates.Walking,
            GameStateManager.GameStates.LoseState => BaseObservables.RunnerStates.Death,
            GameStateManager.GameStates.WinState => BaseObservables.RunnerStates.Victory,
            _ => _playerObservables.RunnerState.Value
        };
    }

    private void UpdateAnimations(BaseObservables.RunnerStates states)
    {
        if (states == BaseObservables.RunnerStates.Walking)
            _animationHandler.CrossFadeInFixed(Constants.AnimationState.RUN, 0, 0.3F);
        if (states == BaseObservables.RunnerStates.Hit)
            _animationHandler.Play(Constants.AnimationState.HIT);
        if (states == BaseObservables.RunnerStates.Victory)
            _animationHandler.Play(Constants.AnimationState.VICTORY);
    }


    public void Tick()
    {
        Rotate();
        if (_playerObservables.RunnerState.Value == BaseObservables.RunnerStates.Walking)
            MoveForward();
    }


    private void Rotate()
    {
        var direction = GetDirection();
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.LerpAngle(_model.Rigidbody.transform.eulerAngles.y, targetAngle,
            10 * Time.deltaTime * direction.magnitude);
        _model.Rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
    }

    private void MoveForward()
    {
        _model.GO.transform.Translate(_model.GO.transform.forward * Time.deltaTime * 5,
            Space.World);
    }

    private Vector3 GetDirection() => new(_uıSettings.Joystick.Horizontal, 0, _uıSettings.Joystick.Vertical);


    [Serializable]
    public class UISettings
    {
        public Joystick Joystick;
    }
}