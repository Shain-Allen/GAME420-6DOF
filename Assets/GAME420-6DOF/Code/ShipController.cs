using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    private Rigidbody _shipRb;
    private DOFInputActions _dofInputActions;
    [SerializeField] private float _shipLatForce = 10f;
    [SerializeField] private float _shipRollForce = 5f;
    [SerializeField] private float _shipYawForce = 5f;
    [SerializeField] private float _shipPitchForce = 5f;

    [SerializeField] private float _movementAngleDamping = 2f;
    [SerializeField] private float _stationaryAngleDamping = 30f;
    
    private bool _isRolling = false;
    private bool _isYaw = false;
    private bool _isPitch = false;

    private Vector3 _shipLatInput;
    private float _shipRollInput;
    private float _shipYawInput;
    private float _shipPitchInput;
    private Vector2 _shipLookInput;
    
    private void Awake()
    {
        _dofInputActions = new DOFInputActions();
        
        _shipRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _dofInputActions.Player.Enable();

        _dofInputActions.Player.LatMovement.performed += OnLatMovement;
        _dofInputActions.Player.LatMovement.canceled += OnLatMovement;
        _dofInputActions.Player.RollMovement.performed += OnRollMovement;
        _dofInputActions.Player.RollMovement.canceled += OnRollMovement;
        _dofInputActions.Player.PitchMovement.performed += OnPitchMovement;
        _dofInputActions.Player.PitchMovement.canceled += OnPitchMovement;
        _dofInputActions.Player.YawMovement.performed += OnYawMovement;
        _dofInputActions.Player.YawMovement.canceled += OnYawMovement;
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        _dofInputActions.Player.Disable();
        
        _dofInputActions.Player.LatMovement.performed -= OnLatMovement;
        _dofInputActions.Player.LatMovement.canceled -= OnLatMovement;
        _dofInputActions.Player.RollMovement.performed -= OnRollMovement;
        _dofInputActions.Player.RollMovement.canceled -= OnRollMovement;
        _dofInputActions.Player.PitchMovement.performed -= OnPitchMovement;
        _dofInputActions.Player.PitchMovement.canceled -= OnPitchMovement;
        _dofInputActions.Player.YawMovement.performed -= OnYawMovement;
        _dofInputActions.Player.YawMovement.canceled -= OnYawMovement;
    }

    private void FixedUpdate()
    {
        _shipRb.AddForce(transform.TransformDirection(_shipLatInput * _shipLatForce));

        Vector3 combinedTorque = transform.forward * (_shipRollInput * _shipRollForce);
        combinedTorque += transform.up * (_shipYawInput * _shipPitchForce);
        combinedTorque += transform.right * (_shipPitchInput * _shipYawForce);
        
        _shipRb.AddTorque(combinedTorque);

        if (_isRolling || _isYaw || _isPitch)
        {
            _shipRb.angularDamping = _movementAngleDamping;
        }
        else
        {
            _shipRb.angularDamping = _stationaryAngleDamping;
        }
    }

    private void OnLatMovement(InputAction.CallbackContext ctx)
    {
        _shipLatInput = ctx.ReadValue<Vector3>();
    }
    
    private void OnRollMovement(InputAction.CallbackContext ctx)
    {
        _shipRollInput = ctx.ReadValue<float>();

        if (ctx.performed)
        {
            _isRolling = true;
        }
        else if (ctx.canceled)
        {
            _isRolling = false;
        }
    }

    private void OnPitchMovement(InputAction.CallbackContext ctx)
    {
        _shipPitchInput = ctx.ReadValue<float>();

        if (ctx.performed)
        {
            _isPitch = true;
        }
        else if (ctx.canceled)
        {
            _isPitch = false;
        }
    }

    private void OnYawMovement(InputAction.CallbackContext ctx)
    {
        _shipYawInput = ctx.ReadValue<float>();

        if (ctx.performed)
        {
            _isYaw = true;
        }
        else if (ctx.canceled)
        {
            _isYaw = false;
        }
    }
}
