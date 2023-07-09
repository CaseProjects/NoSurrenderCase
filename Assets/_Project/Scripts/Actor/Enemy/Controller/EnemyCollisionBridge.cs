using Events;

public class EnemyCollisionBridge : BaseCollisionBridge
{
    protected override void OnDeath()
    {
        _signalBus.Fire<SignalEnemyDied>();
    }
}