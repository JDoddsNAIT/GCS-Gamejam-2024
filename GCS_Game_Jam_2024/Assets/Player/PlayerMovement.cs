using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] private Rigidbody2D _rigidbody = null;
    private Vector2 _movement = Vector2.zero;
    [SerializeField] private float _moveSpeedX = 10.0f;
    private bool _jump = false;


    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    private void Start()
    {
        Debug.Log("New Player Joined");
    }
    void Update()
    {
        _movement.x = _playerInput.actions["Movement"].ReadValue<Vector2>().x * _moveSpeedX;
        _jump = _playerInput.actions["Jump"].WasPressedThisFrame();

        _movement.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _movement;
    }
}
