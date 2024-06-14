using Player;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor _motor;
    private PlayerLook _look;
    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        
        onFoot.Jump.performed += ctx => _motor.Jump();
        onFoot.Crouch.performed += ctx => _motor.Crouch();
        onFoot.Sprint.performed += ctx => _motor.Sprint();
    }

    private void FixedUpdate()
    {
        _motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
