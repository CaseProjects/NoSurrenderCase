using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyBehavior
{
    public class EnemyModel : BaseModel
    {
        public NavMeshAgent NavMeshAgent { get; }

        public EnemyModel(Animator animator, Rigidbody rigidbody, NavMeshAgent navMeshAgent)
            : base(animator, rigidbody)
        {
            NavMeshAgent = navMeshAgent;
        }
    }
}