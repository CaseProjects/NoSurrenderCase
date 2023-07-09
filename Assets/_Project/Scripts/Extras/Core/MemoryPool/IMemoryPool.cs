namespace Core.MemoryPool
{
    public interface IMemoryPool
    {
        void Despawn(object item);
        void Clear();
    }

    public interface IMemoryPool<out TValue> : IMemoryPool
    {
        TValue Spawn();
    }

    public interface IMemoryPool<in TParam1, out TValue> : IMemoryPool
    {
        TValue Spawn(TParam1 arg1);
    }
}