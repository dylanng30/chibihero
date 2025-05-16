using UnityEngine;

public interface IProjectileTrajectory
{
    Vector2 GetInitialVelocity(int dmg, Transform target, Transform origin);
}
