using System.Threading.Tasks;
using DG.Tweening;
using Events;
using UnityEngine;
using Zenject;

public class HitController : IInitializable
{
    #region INJECT

    private readonly BaseModel _baseModel;
    private readonly SignalBus _signalBus;
    private readonly BaseObservables _baseObservables;

    public HitController(SignalBus signalBus, BaseModel baseModel, BaseObservables baseObservables)
    {
        _signalBus = signalBus;
        _baseModel = baseModel;
        _baseObservables = baseObservables;
    }

    #endregion

    public void Initialize()
    {
        _signalBus.Subscribe<SignalTakeHit>(hitData => OnTakeHit(hitData));
    }

    private async Task OnTakeHit(SignalTakeHit hitData)
    {
        _baseObservables.RunnerState.Value = BaseObservables.RunnerStates.Hit;

        var strengthDiff = hitData.StrengthLevel - _baseObservables.StrengthLevel.Value;
        var strengthRange = Mathf.InverseLerp(-5, 5, strengthDiff);
        var hitMultiplier = Mathf.Lerp(1, 10, strengthRange);


        //Check if hit from back
        if (Vector3.Dot(hitData.HitForward, _baseModel.GO.transform.forward) < -0.5F)
            _baseModel.Rigidbody.AddForce(-hitData.HitForward * hitMultiplier * 200);
        else
            _baseModel.Rigidbody.AddForce(-hitData.HitForward * hitMultiplier * 100);

        PunchScale();

        await Task.Delay(100);

        while (_baseModel.Rigidbody.velocity.magnitude > 0.1f)
            await Task.Yield();

        _baseModel.Rigidbody.velocity = Vector3.zero;
        _baseObservables.RunnerState.Value = BaseObservables.RunnerStates.Walking;
    }

    private void PunchScale()
    {
        var targetScale = Vector3.one + Vector3.one * 0.1f;
        _baseModel.GO.transform.DOPunchScale(targetScale * 0.2f, 0.1f, 2, 0.2f)
            .SetEase(Ease.OutBack);
    }
}