using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace MainHandlers.UI
{
    public class UIManager : IInitializable
    {
        #region INJECT

        private readonly BaseSettings _baseSettings;
        private readonly GameStateManager _gameStateManager;


        private UIManager(BaseSettings baseSettings, GameStateManager gameStateManager)
        {
            _baseSettings = baseSettings;
            _gameStateManager = gameStateManager;
        }

        #endregion

        private GameObject _activeUI;

        public void Initialize()
        {
            SetGameStartup();
            ChangeUIWithGameState();

            void ChangeUIWithGameState()
            {
                _gameStateManager.GameStateReactiveProperty.Subscribe(ChangeUI);
            }
        }


        private void SetGameStartup()
        {
            SetButtons();
        }


        private void SetButtons()
        {
            _baseSettings.StartButton.onClick.AddListener(() =>
                _gameStateManager.ChangeState(GameStateManager.GameStates.WarmupState));

            _baseSettings.NextLevelButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        }


        private void ChangeUI(GameStateManager.GameStates state)
        {
            _activeUI?.SetActive(false);
            _activeUI = null;
            _activeUI = GetCurrentUI(state);
            _activeUI?.SetActive(true);
        }


        private GameObject GetCurrentUI(GameStateManager.GameStates states)
        {
            return _baseSettings.Panels.Single(x => x.State == states).Panel;
        }

        [Serializable]
        public class UIPanelData
        {
            public GameStateManager.GameStates State;
            public GameObject Panel;
        }

        [Serializable]
        public class BaseSettings
        {
            [TabGroup("PANELS")] public UIPanelData[] Panels;

            [TabGroup("BUTTONS")] public Button StartButton;
            [TabGroup("BUTTONS")] public Button NextLevelButton;
        }
    }
}