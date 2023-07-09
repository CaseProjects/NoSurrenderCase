using UnityEngine;

namespace Constants
{
    public static class Tags
    {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string DeathZone = "DeathZone";
    }


    public static class AnimationState
    {
        public static readonly int IDLE = Animator.StringToHash("IDLE");
        public static readonly int RUN = Animator.StringToHash("RUN");
        public static readonly int HIT = Animator.StringToHash("HIT");
        public static readonly int VICTORY = Animator.StringToHash("VICTORY");
    }
}