using System;
using Constants;
using DG.Tweening;
using Events;
using UnityEngine;
using Zenject;

public class CollectableFood : MonoBehaviour, ICollectable, IPoolable<IMemoryPool>, IDisposable
{
    [Inject] private SignalBus _signalBus;
    private IMemoryPool _pool;
    private Vector3 _defaultScale;

    private Collider _collider;

    #region POOL

    public void OnDespawned()
    {
        transform.localScale = _defaultScale;
        _collider.enabled = true;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
    }

    #endregion

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _defaultScale = transform.localScale;
        transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void Dispose()
    {
        _signalBus.Fire(new SignalFoodCollected());
        _pool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player) && !other.CompareTag(Tags.Enemy)) return;

        DestroyFood();
    }

    private void DestroyFood()
    {
        _collider.enabled = false;
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(Dispose);
    }


    public class Factory : PlaceholderFactory<CollectableFood>
    {
    }
}

