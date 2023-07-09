using Events;
using UnityEngine;
using Zenject;

public abstract class BaseCollisionBridge : MonoBehaviour, IHittable
{
    #region INJECT

    private protected BaseObservables _observables;
    private protected SignalBus _signalBus;


    [Inject]
    private void Construct(BaseObservables observables, SignalBus signalBus)
    {
        _observables = observables;
        _signalBus = signalBus;
    }

    #endregion

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IHittable target))
            target.TakeHit(_observables.StrengthLevel.Value, other.GetContact(0).normal);
    }

    public void TakeHit(int strengthLevel, Vector3 hitForward)
    {
        _signalBus.Fire(new SignalTakeHit(strengthLevel, hitForward));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable _))
            OnCollectFood();

        if (other.CompareTag(Constants.Tags.DeathZone))
            OnDeath();
    }

    protected virtual void OnCollectFood()
    {
        _observables.StrengthLevel.Value++;
    }

    protected abstract void OnDeath();
}

