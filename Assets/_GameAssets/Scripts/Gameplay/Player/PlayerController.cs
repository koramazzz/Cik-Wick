using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChanged;

    [Header("References")]
    [SerializeField] private Transform _orientationTransform;


    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;


    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;


    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;


    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float _groundDrag;


    private StateController _stateController;
    private Rigidbody _playerRigidbody;

    private float startingMovementSpeed, startingJumpForce;
    private float _horizontalInput, _verticalInput;

    private Vector3 _movementDirection;

    private bool _isSliding;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        startingMovementSpeed = _movementSpeed;
        startingJumpForce = _jumpForce;
    }

    private void Update()
    {
        if (!GameManager.IsGameplayActive)
        {
            return;
        }
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsGameplayActive)
        {
            return;
        }
        
        SetPlayerMovement();
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentPlayerState = _stateController.GetCurrentPlayerState();

        var newplayerState = currentPlayerState switch
        {
            _ when !isGrounded => PlayerState.Jump,
            _ when isSliding && movementDirection != Vector3.zero => PlayerState.Slide,
            _ when isSliding && movementDirection == Vector3.zero => PlayerState.SlideIdle,
            _ when movementDirection == Vector3.zero && isGrounded => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded => PlayerState.Move,
            _ => currentPlayerState
        };
        
        if (newplayerState != currentPlayerState) 
        {
            _stateController.ChangePlayerState(newplayerState);
            OnPlayerStateChanged?.Invoke(newplayerState);
        }
    }

    private void SetInputs() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }

        if (Input.GetKeyUp(_movementKey))
        {
            _isSliding = false;
        }

        if (Input.GetKeyDown(_jumpKey) && IsGrounded())
        {
            SetPlayerJumping();
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentPlayerState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentPlayerState() switch
        {
            PlayerState.Idle => _groundDrag,
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.SlideIdle => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };
        
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();

        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    #region Boost Methods
    public void SetMovementSpeed(float speedBoostAmount, float duration)
    {
        _movementSpeed += speedBoostAmount;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = startingMovementSpeed;
    }

    public void SetJumpForce(float jumpBoostAmount, float duration)
    {
        _jumpForce += jumpBoostAmount;
        Invoke(nameof(ResetJumpForce), duration);
    }

    private void ResetJumpForce()
    {
        _jumpForce = startingJumpForce;
    }
    #endregion

    #region Helper Methods
    private bool IsGrounded()
    {
        return Physics.SphereCast(transform.position, 0.2f, Vector3.down, out _, _playerHeight / 2 + 0.3f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidbody;
    }
    #endregion
}
