using UnityEngine;

namespace PlayerBehaviors
{
    public class PlayerModel : BaseModel
    {
        public PlayerModel(Rigidbody rigidBody, Animator animator) : base(animator, rigidBody)
        {
        }
    }
}