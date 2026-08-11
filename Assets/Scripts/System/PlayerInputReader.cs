using System;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoSingleton<PlayerInputReader>,
    IInputMove, IInputCamera, IInputWheel, IInputAlt
{
    //IInputMove
    public Vector2 Direction => _directionInput;
    public bool Sprint => _sprintInput;

    //IInputCamera
    public Vector2 CameraInput => _cameraInput;

    //IInputWheel
    public float Wheel => _wheelInput;

    public bool Alt => _altInput;

    //actions
    public event Action<float> OnWheelInput;
    public event Action OnBuildInput;
    public event Action OnConfirmInput;
        


    private PlayerInput _input;

    private Vector2 _directionInput;
    private Vector2 _cameraInput;
    private float _wheelInput;
    private bool _sprintInput;
    private bool _altInput;
    

    protected override void Awake()
    {
        base.Awake();
        LoadAssetInputAction();
    }

    private void OnEnable()
    {
        Binding();
    }
    private void OnDisable()
    {
        Unbinding();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _directionInput = context.ReadValue<Vector2>().normalized;
#if UNITY_EDITOR
        Debug.Log($"InputReader get Dir : {Direction}");
#endif
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        _sprintInput = context.ReadValueAsButton();
#if UNITY_EDITOR
        Debug.Log($"InputReader get Sprint : {Sprint}");
#endif
    }

    public void OnLook(InputAction.CallbackContext context)
    {

        _cameraInput = context.ReadValue<Vector2>();
#if UNITY_EDITOR
        Debug.Log($"InputReader Camera : {CameraInput}");
#endif
    }

    public void OnWheel(InputAction.CallbackContext context)
    {
        _wheelInput = context.ReadValue<float>();
        OnWheelInput?.Invoke(_wheelInput);
#if UNITY_EDITOR
        Debug.Log($"InputReader Get Wheel is : {_wheelInput}");
#endif
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        OnConfirmInput?.Invoke();
#if UNITY_EDITOR
        Debug.Log("InputReader get event Attack");
#endif

    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        OnBuildInput?.Invoke();
#if UNITY_EDITOR
        Debug.Log("InputReader get event Build");
#endif
    }
    public void OnAlt(InputAction.CallbackContext context)
    {
        _altInput= context.ReadValueAsButton();
#if UNITY_EDITOR
        Debug.Log($"Alt is : {_altInput}");
#endif
    }

    private void LoadAssetInputAction()
    {
        if (_input == null)
        {
            if (!TryGetComponent<PlayerInput>(out _input))
            {
                _input = gameObject.AddComponent<PlayerInput>();

                InputActionAsset actionAsset = Resources.Load<InputActionAsset>("Input/PlayerInputActions");
                if (actionAsset != null)
                {
                    _input.actions = actionAsset;

                }
                _input.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
            }
        }
    }
    private void Binding()
    {
        _input.actions["Player/Move"].performed += OnMove;
        _input.actions["Player/Move"].canceled += OnMove;

        _input.actions["Player/Sprint"].performed += OnSprint;
        _input.actions["Player/Sprint"].canceled += OnSprint;


        _input.actions["Player/Look"].performed += OnLook;
        _input.actions["Player/Look"].canceled += OnLook;

        _input.actions["Player/Wheel"].performed += OnWheel;


        _input.actions["Player/Attack"].performed += OnAttack;
        _input.actions["Player/Build"].started += OnBuild;

        _input.actions["Player/Alt"].performed += OnAlt;
        _input.actions["Player/Alt"].canceled += OnAlt;

        OnWheelInput = null;
        OnBuildInput = null;
        OnConfirmInput = null;
    }
    private void Unbinding()
    {
        _input.actions["Player/Move"].performed -= OnMove;
        _input.actions["Player/Move"].canceled -= OnMove;

        _input.actions["Player/Sprint"].performed -= OnSprint;
        _input.actions["Player/Sprint"].canceled -= OnSprint;


        _input.actions["Player/Look"].performed -= OnLook;
        _input.actions["Player/Look"].canceled -= OnLook;

        _input.actions["Player/Wheel"].performed -= OnWheel;
        _input.actions["Player/Build"].started -= OnBuild;

        _input.actions["Player/Alt"].performed -= OnAlt;
        _input.actions["Player/Alt"].canceled -= OnAlt;

        OnWheelInput = null;
        OnBuildInput = null;
        OnConfirmInput = null;
    }
}
