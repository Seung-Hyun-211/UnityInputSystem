using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private IInputMove _moveInput;
    private IInputCamera _cameraInput;
    private ICameraFollow _cameraController;

    private CharacterController _characterController;

    [SerializeField]
    private float normalSpeed = 7f;
    [SerializeField]
    private float splintMultiply = 1.3f;
    [SerializeField]
    private float rotateMultiply = 0.2f;
    [SerializeField]
    private Vector3 positionDiff = new Vector3(1.5f, 2f, -1.5f);

    private float _rotateX;
    private float _rotateY;


    private void Awake()
    {
        _moveInput = PlayerInputReader.Instance;
        _cameraInput = PlayerInputReader.Instance;
        _cameraController = CameraController.Instance;
        _cameraController.SetFollow(this.transform, positionDiff);

        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveCharacter();
        RotationCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 movement = new Vector3(_moveInput.Direction.x, 0f, _moveInput.Direction.y) * normalSpeed;

        if (_moveInput.Sprint)
            movement *= splintMultiply;

        _characterController.Move((Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * movement) * Time.deltaTime);
    }

    private void RotationCharacter()
    {
        _rotateY += _cameraInput.CameraInput.x * rotateMultiply;
        _rotateX -= _cameraInput.CameraInput.y * rotateMultiply;
        _rotateX = Mathf.Clamp(_rotateX, -70, 70);

        Quaternion rotation = Quaternion.Euler(new Vector3(0f, _rotateY, 0f));
        transform.rotation = rotation;

        Quaternion camRot = Quaternion.Euler(_rotateX, transform.rotation.eulerAngles.y, 0f);
        _cameraController.Rotation(camRot);
    }
}
