using UnityEngine;

public interface Ihittable
{
    void ApplyHit(HitInfo hitInfo);
}
public struct HitInfo
{
    public Vector3 HitPoint;
    public Vector3 HitNormal;
    public float Damage;
}
