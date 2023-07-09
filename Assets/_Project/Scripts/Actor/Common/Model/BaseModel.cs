using UnityEngine;

public abstract class BaseModel
{
    protected BaseModel(Animator animator, Rigidbody rigidbody)
    {
        GetAnimator = animator;
        Rigidbody = rigidbody;
    }

    public Animator GetAnimator { get; }
    public GameObject GO => GetAnimator.gameObject;

    public Rigidbody Rigidbody { get; }
}