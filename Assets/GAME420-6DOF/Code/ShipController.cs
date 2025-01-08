using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] Rigidbody _shipRb;
    [SerializeField] private float _shipLatForce = 10f;
    [SerializeField] private float _shipRollForce = 5f;
    
    DOFInputActions _dofInputActions;

    private void Awake()
    {
        _dofInputActions = new DOFInputActions();
        
        _shipRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
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
        _dofInputActions.Player.Disable();
        
        _dofInputActions.Player.LatMovement.performed -= OnLatMovement;
        _dofInputActions.Player.LatMovement.canceled -= OnLatMovement;
        _dofInputActions.Player.RollMovement.performed -= OnRollMovement;
        _dofInputActions.Player.RollMovement.canceled -= OnLatMovement;
        _dofInputActions.Player.Look.performed -= OnLook;
        _dofInputActions.Player.Look.canceled -= OnLook;
    }
    
    private void OnLatMovement(InputAction.CallbackContext ctx)
    {
        _shipRb.AddForce(ctx.ReadValue<Vector3>() * _shipLatForce);
    }
    
    private void OnRollMovement(InputAction.CallbackContext ctx)
    {
        _shipRb.AddTorque(transform.forward * ctx.ReadValue<float>() * _shipRollForce);
    }
    
    private void OnLook(InputAction.CallbackContext ctx)
    {
        _shipRb.AddTorque(transform.up * ctx.ReadValue<Vector2>().x * _shipRollForce);
        _shipRb.AddTorque(transform.right * ctx.ReadValue<Vector2>().y * _shipRollForce);
    }
}
