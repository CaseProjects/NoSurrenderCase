using Events;
using MainHandlers;
using Zenject;

public class PlayerCollisionBridge : BaseCollisionBridge
{
    [Inject] private protected GameStateManager _gameStateManager;

    protected override void OnDeath()
    {
        _gameStateManager.ChangeState(GameStateManager.GameStates.LoseState);
    }

    protected override void OnCollectFood()
    {
        base.OnCollectFood();
        _signalBus.Fire(new SignalUpdateScore(_observables.StrengthLevel.Value * 100));
    }
}