using UnityEngine;

namespace Events
{
    public struct SignalFoodCollected
    {
    }

    public struct SignalTakeHit
    {
        public int StrengthLevel { get; }
        public Vector3 HitForward { get; }

        public SignalTakeHit(int strengthLevel, Vector3 hitForward)
        {
            StrengthLevel = strengthLevel;
            HitForward = hitForward;
        }
    }

    public struct SignalEnemyDied
    {
    }

    public struct SignalUpdateScore
    {
        public SignalUpdateScore(int score)
        {
            Score = score;
        }

        public int Score { get; }
    }
}