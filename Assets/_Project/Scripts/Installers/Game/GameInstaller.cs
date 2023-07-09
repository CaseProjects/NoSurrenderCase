using MainHandlers;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GameSignalsInstaller.Install(Container);
            InstallMainBehaviors();
        }


        private void InstallMainBehaviors()
        {
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
        }
    }
}