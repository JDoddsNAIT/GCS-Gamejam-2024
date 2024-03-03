using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] private Rigidbody2D _rigidbody = null;
    private Vector2 _movement = Vector2.zero;
    [SerializeField] private float _moveSpeedX = 10.0f;
    [SerializeField] private float _jumpStrength = 10.0f;
    private bool _hasDoubleJump = false;
    private bool _jump = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.Log("New Player Joined");
    }
    void Update()
    {
        _movement.x = _playerInput.actions["Movement"].ReadValue<Vector2>().x;
        _jump = _playerInput.actions["Jump"].WasPressedThisFrame();

        if (_jump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpStrength);
        }

        _rigidbody.velocity = new Vector2(_movement.x * _moveSpeedX, _rigidbody.velocity.y);
    }

    public void HitJumpPad()
    {
        Debug.Log("Jump Pad Force Applied!");
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 25);
        }
    }
}
