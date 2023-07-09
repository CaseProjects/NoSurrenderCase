using System;
using System.Threading.Tasks;
using DG.Tweening;
using MainHandlers;
using TMPro;
using UnityEngine;
using Zenject;

public class WarmupController : IInitializable
{
    #region INJECT

    private readonly Settings _settings;
    private readonly GameStateManager _gameStateManager;

    public WarmupController(Settings settings, GameStateManager gameStateManager)
    {
        _settings = settings;
        _gameStateManager = gameStateManager;
    }

    #endregion

    public void Initialize()
    {
        _gameStateManager.GameStateReactiveProperty.Subscribe(gameState =>
        {
            if (gameState == GameStateManager.GameStates.WarmupState)
                ActivateWarmupText();
        });
    }

    private async Task ActivateWarmupText()
    {
        for (var i = 3; i > 0; i--)
        {
            UpdateTextAndPunch(i.ToString());
            await Task.Delay(1000);
        }

        UpdateTextAndPunch("GO!");
        await Task.Delay(1000);
        _gameStateManager.ChangeState(GameStateManager.GameStates.InGameState);
    }

    private void UpdateTextAndPunch(string text)
    {
        _settings.WarmupText.text = text;
        _settings.WarmupText.transform.DOPunchScale(Vector3.one * 5f, 0.5f, 1, 0.5f);
    }

    [Serializable]
    public class Settings
    {
        public TextMeshProUGUI WarmupText;
    }
}