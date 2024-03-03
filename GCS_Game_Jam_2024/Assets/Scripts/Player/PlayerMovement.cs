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
    private bool _isGrounded = true;
    [SerializeField] private Animator[] _animator = new Animator[4];
    [SerializeField] private float _castDistance;
    [SerializeField] private float _castRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private ParticleSystem _dustEffects;
    private int _playerIndex = 0;

    void Update()
    {
        _movement.x = _playerInput.actions["Movement"].ReadValue<Vector2>().x;
        _jump = _playerInput.actions["Jump"].WasPressedThisFrame();
        _isGrounded = CheckGrounded();

        if (_isGrounded)
        {
            _dustEffects.Play();
            
        }
        else
        {
            _dustEffects.Stop();
        }

        if (_jump && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpStrength);
        }

        _rigidbody.velocity = new Vector2(_movement.x * _moveSpeedX, _rigidbody.velocity.y);
        _animator[_playerIndex].SetFloat("Speed", Mathf.Abs(_movement.x));
        _animator[_playerIndex].SetBool("Jumping", !_isGrounded);
        if (_movement.x > 0) { _animator[_playerIndex].transform.localScale = new Vector3(1, 1, 1); }
        else if (_movement.x < 0) { _animator[_playerIndex].transform.localScale = new Vector3(-1, 1, 1); }
    }

    private bool CheckGrounded()
    {
        return Physics2D.CircleCast(transform.position, _castRadius, -transform.up, _castDistance, _groundLayer);
    }

    public void ResetMovement(Vector3 pos)
    {
        transform.position = pos;
        _movement = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
    }

    public void HitJumpPad()
    {
        Debug.Log("Jump Pad Force Applied!");
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 25);
        }
    }

    public void SetID(int id)
    {
        _playerIndex = id;

        for (int i = 0; i < _playerIndex; i++) { _animator[i].gameObject.SetActive(false);  }
        _animator[_playerIndex].gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (_isGrounded)  { Gizmos.color = Color.green; }
        else { Gizmos.color = Color.red; }

        Gizmos.DrawWireSphere(transform.position - (transform.up * _castDistance), _castRadius);
    }
}
