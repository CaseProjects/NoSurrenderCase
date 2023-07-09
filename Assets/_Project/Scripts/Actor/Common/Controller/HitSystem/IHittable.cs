using UnityEngine;

public interface IHittable
{
    void TakeHit(int strengthLevel, Vector3 hitForward);
}