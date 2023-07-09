using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MainHandlers
{
    public class GameStateManager : IInitializable
    {
        public enum GameStates
        {
            IdleState,
            WarmupState,
            InGameState,
            LoseState,
            WinState,
            None
        }

        public readonly ReactiveProperty<GameStates> GameStateReactiveProperty = new(GameStates.None);

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            ChangeStateIdleWithDelay();
        }

        private async Task ChangeStateIdleWithDelay()
        {
            await Task.Delay(10);
            ChangeState(GameStates.IdleState);
        }


        public void ChangeState(GameStates nextState)
        {
            GameStateReactiveProperty.Value = nextState;
        }
    }
}