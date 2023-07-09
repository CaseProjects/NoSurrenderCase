using UnityEngine;

namespace Core.MemoryPool
{
    public class MonoPoolableMemoryPool<TParam1, TComponent> : MonoMemoryPoolBase<TComponent>
        where TComponent : Component, IPoolable<TParam1, IMemoryPool>
    {
        public MemoryPool<TParam1, TComponent> Pool { get; private set; }

        private void OnTakeFromPool(TComponent obj, TParam1 arg)
        {
            obj.gameObject.SetActive(true);
            obj.OnSpawned(arg, Pool);
        }

        protected override void InitializePool()
        {
            Pool = new MemoryPool<TParam1, TComponent>(CreatePooledItem, OnTakeFromPool,
                OnReturnedToPool, OnDestroyPoolObject);
            for (var i = 0; i < _initialPoolSize; i++) Pool.Despawn(CreatePooledItem());
        }
    }

    public class MonoMemoryPool<TComponent> : MonoMemoryPoolBase<TComponent>
        where TComponent : Component
    {
        public MemoryPool<TComponent> Pool { get; private set; }


        protected override void InitializePool()
        {
            Pool = new MemoryPool<TComponent>(CreatePooledItem, OnTakeFromPool,
                OnReturnedToPool, OnDestroyPoolObject);

            for (var i = 0; i < _initialPoolSize; i++) Pool.Despawn(CreatePooledItem());
        }
    }


    public abstract class MonoMemoryPoolBase<TComponent> : MonoBehaviour
        where TComponent : Component
    {
        [SerializeField] protected TComponent _prefab;
        [SerializeField] protected int _initialPoolSize = 10;

        private void Awake()
        {
            InitializePool();
        }

        protected abstract void InitializePool();

        private protected TComponent CreatePooledItem()
        {
            return Instantiate(_prefab, transform, true);
        }

        private protected void OnReturnedToPool(TComponent obj)
        {
            obj.gameObject.SetActive(false);
        }


        private protected void OnTakeFromPool(TComponent obj)
        {
            obj.gameObject.SetActive(true);
        }

        private protected void OnDestroyPoolObject(TComponent obj)
        {
            Destroy(obj.gameObject);
        }
    }
}