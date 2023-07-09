using System;
using Events;
using TMPro;
using Zenject;

public class PlayerScoreController : IInitializable
{
    #region INJECT

    private readonly Settings _settings;
    private readonly SignalBus _signalBus;

    public PlayerScoreController(Settings settings, SignalBus signalBus)
    {
        _settings = settings;
        _signalBus = signalBus;
    }

    #endregion

    public void Initialize()
    {
        _signalBus.Subscribe<SignalUpdateScore>(x => UpdateScoreText(x.Score));
    }

    private void UpdateScoreText(int score)
    {
        _settings.ScoreText.text = score.ToString();
    }

    [Serializable]
    public class Settings
    {
        public TextMeshProUGUI ScoreText;
    }
}