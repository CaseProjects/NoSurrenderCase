using DG.Tweening;
using UnityEngine;
using Zenject;

public class StrengthController : IInitializable
{
    #region INJECT

    private readonly BaseObservables _baseObservables;
    private readonly BaseModel _baseFacade;

    public StrengthController(BaseObservables baseObservables, BaseModel baseFacade)
    {
        _baseObservables = baseObservables;
        _baseFacade = baseFacade;
    }

    #endregion

    public void Initialize()
    {
        _baseObservables.StrengthLevel.Subscribe(IncreaseScale);
    }

    private void IncreaseScale(int strengthLevel)
    {
        var scaleValue = Vector3.one + Vector3.one * ((strengthLevel + 1) * 0.2f);
        _baseFacade.GO.transform.DOPunchScale(scaleValue * 0.3f, 0.2f, 1, 0.5f)
            .SetEase(Ease.OutBack).onComplete += () => _baseFacade.GO.transform.localScale = scaleValue;
    }
}