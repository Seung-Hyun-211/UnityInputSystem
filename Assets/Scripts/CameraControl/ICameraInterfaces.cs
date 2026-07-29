using UnityEngine;

public interface ICameraFollow
{
    void SetFollow(Transform target, Vector3 position);
    void Rotation(Quaternion euler);
}

public interface ICameraRaycastHit
{
    RaycastHit RaycastHit { get; }
}