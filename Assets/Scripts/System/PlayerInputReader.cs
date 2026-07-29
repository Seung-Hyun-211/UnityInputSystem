using System;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoSingleton<PlayerInputReader>, IInputMove, IInputCamera, IInputWheel
{
    //IInputMove
    public Vector2 Direction => _directionInput;
    public bool Sprint => _sprintInput;

    //IInputCamera
    public Vector2 CameraInput => _cameraInput;

    //IInputWheel
    public float Wheel => _wheelInput;

    //actions
    public event Action OnBuildInput;
    public event Action OnConfirmInput;
        


    private PlayerInput _input;

    private Vector2 _directionInput;
    private Vector2 _cameraInput;
    private float _wheelInput;
    private bool _sprintInput;
    

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
        Debug.Log($"Get Dir : {Direction}");
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        _sprintInput = context.ReadValueAsButton();
        Debug.Log($"Sprint is : {Sprint}");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
        Debug.Log($"Camera is : {CameraInput}");
    }

    public void OnWheel(InputAction.CallbackContext context)
    {
        _wheelInput = context.ReadValue<float>();
        Debug.Log($"Wheel is : {Wheel}");
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("event Attack");
        
        OnConfirmInput?.Invoke();
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        Debug.Log("event Build");
        OnBuildInput?.Invoke();
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

        OnBuildInput = null;
        OnConfirmInput = null;
    }
}
