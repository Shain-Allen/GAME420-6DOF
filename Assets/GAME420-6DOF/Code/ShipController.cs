using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] Rigidbody _shipRb;
    DOFInputActions _dofInputActions;
    [SerializeField] private float _shipLatForce = 10f;
    [SerializeField] private float _shipRollForce = 5f;
    [SerializeField] private float _shipYawForce = 5f;
    [SerializeField] private float _shipPitchForce = 5f;

    private Vector3 _shipLatInput;
    private float _shipRollInput;
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
        _dofInputActions.Player.RollMovement.canceled += OnLatMovement;
        _dofInputActions.Player.Look.performed += OnLook;
        _dofInputActions.Player.Look.canceled += OnLook;
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        _dofInputActions.Player.Disable();
        
        _dofInputActions.Player.LatMovement.performed -= OnLatMovement;
        _dofInputActions.Player.LatMovement.canceled -= OnLatMovement;
        _dofInputActions.Player.RollMovement.performed -= OnRollMovement;
        _dofInputActions.Player.RollMovement.canceled -= OnLatMovement;
        _dofInputActions.Player.Look.performed -= OnLook;
        _dofInputActions.Player.Look.canceled -= OnLook;
    }

    private void Update()
    {
        _shipRb.AddForce(transform.TransformDirection(_shipLatInput * (_shipLatForce * Time.deltaTime)));
        _shipRb.AddTorque(transform.forward * (_shipRollInput * _shipRollForce));
        _shipRb.AddTorque(transform.up * (_shipLookInput.x * _shipPitchForce));
        _shipRb.AddTorque(transform.right * (_shipLookInput.y * _shipYawForce));
    }

    private void OnLatMovement(InputAction.CallbackContext ctx)
    {
        _shipLatInput = ctx.ReadValue<Vector3>();
    }
    
    private void OnRollMovement(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<float>());
        _shipRollInput = ctx.ReadValue<float>();
    }
    
    private void OnLook(InputAction.CallbackContext ctx)
    {
        _shipLookInput = ctx.ReadValue<Vector2>();
    }
}
