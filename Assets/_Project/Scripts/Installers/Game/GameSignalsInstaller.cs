using Events;
using Zenject;

namespace Installers
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            InstallGameSignals();
        }

        private void InstallGameSignals()
        {
            Container.DeclareSignal<SignalFoodCollected>().OptionalSubscriber();
            Container.DeclareSignal<SignalUpdateScore>().OptionalSubscriber();
            Container.DeclareSignal<SignalEnemyDied>().OptionalSubscriber();
        }
    }
}