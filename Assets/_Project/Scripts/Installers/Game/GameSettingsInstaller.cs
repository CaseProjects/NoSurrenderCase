using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    [InlineEditor()]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallFoodPool();
        }

        private void InstallFoodPool()
        {
            Container.BindFactory<CollectableFood, CollectableFood.Factory>()
                .FromPoolableMemoryPool<CollectableFood, FoodPool>(b => b
                    .WithInitialSize(15)
                    .FromComponentInNewPrefab(_settings.FoodPrefab)
                    .UnderTransformGroup("EnemyPool"));
        }


        private class FoodPool : MonoPoolableMemoryPool<IMemoryPool, CollectableFood>
        {
        }

        [Serializable]
        private class Settings
        {
            public GameObject FoodPrefab;
        }
    }
}