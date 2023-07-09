using UnityEngine;
using Zenject;

public abstract class BaseFacade : MonoBehaviour
{
    #region INJECT

    private protected BaseModel _model;

    [Inject]
    public void Construct(BaseModel baseModel)
    {
        _model = baseModel;
    }

    #endregion


    public Animator Animator => _model.GetAnimator;


    public Rigidbody Rigidbody => _model.Rigidbody;

    public GameObject GO => _model.GO;
}