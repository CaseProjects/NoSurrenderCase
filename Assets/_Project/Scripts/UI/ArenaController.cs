using System;
using System.Threading.Tasks;
using Events;
using MainHandlers;
using TMPro;
using Zenject;
using Object = UnityEngine.Object;

public class ArenaController : IInitializable
{
    #region INJECT

    private readonly SignalBus _signalBus;
    private readonly Settings _settings;
    private readonly GameStateManager _gameStateManager;

    public ArenaController(SignalBus signalBus, Settings settings, GameStateManager gameStateManager)
    {
        _signalBus = signalBus;
        _settings = settings;
        _gameStateManager = gameStateManager;
    }

    #endregion

    private int _totalPlayer;

    public void Initialize()
    {
        _signalBus.Subscribe<SignalEnemyDied>(_ => UpdatePlayerCountText());

        _gameStateManager.GameStateReactiveProperty.Subscribe(gameState =>
        {
            if (gameState == GameStateManager.GameStates.InGameState) StartTimer();
        });

        _totalPlayer = Object.FindObjectsOfType<BaseFacade>().Length + 1;
        UpdatePlayerCountText();
    }

    private async Task StartTimer()
    {
        var remainingTime = 90;
        while (remainingTime > 0)
        {
            _settings.TimerText.text = $"{(remainingTime / 60):00}:{(remainingTime % 60):00}";
            await Task.Delay(1000);
            remainingTime--;
        }

        if (_gameStateManager.GameStateReactiveProperty.Value == GameStateManager.GameStates.InGameState)
            _gameStateManager.ChangeState(GameStateManager.GameStates.WinState);
    }

    private void UpdatePlayerCountText()
    {
        DecreasePlayerCount();
        _settings.PlayerCountText.text = _totalPlayer.ToString();
    }

    private void DecreasePlayerCount()
    {
        _totalPlayer--;
        if (_totalPlayer == 1)
            if (_gameStateManager.GameStateReactiveProperty.Value == GameStateManager.GameStates.InGameState)
                _gameStateManager.ChangeState(GameStateManager.GameStates.WinState);
    }

    [Serializable]
    public class Settings
    {
        public TextMeshProUGUI PlayerCountText;
        public TextMeshProUGUI TimerText;
    }
}