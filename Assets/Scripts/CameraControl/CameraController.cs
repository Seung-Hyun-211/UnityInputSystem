using UnityEngine;

public class CameraController : MonoSingleton<CameraController>, ICameraFollow, ICameraRaycastHit
{
    public RaycastHit RaycastHit => _raycastHit;

    private Camera _mainCamera;

    //ICameraFollow
    private Transform _target;
    private Vector3 _positionDiff;

    //ICameraRaycast
    private RaycastHit _raycastHit;


    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out _raycastHit, 10f);
    }
    private void LateUpdate()
    {
        FollowingTarget();
    }

    //ICameraFollow Interface
    public void SetFollow(Transform target, Vector3 position)
    {
        _target = target;
        _positionDiff = position;
    }
    //ICameraFollow Interface
    public void Rotation(Quaternion euler)
    {
        _mainCamera.transform.rotation = euler;
    }

    private void FollowingTarget()
    {
        if (_target)
        {
            Vector3 newDif = Quaternion.Euler(0f, _target.rotation.eulerAngles.y, 0f) * _positionDiff;
            _mainCamera.transform.position = _target.transform.position + newDif;
        }
    }
}