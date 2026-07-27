using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _moveSpeed = PlayerManager.Instance.PlayerSpeed;
    }

    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _movement * _moveSpeed;
    }

    public void StopMovement()
    {
        _movement = Vector2.zero;
        _rb.linearVelocity = Vector2.zero;
    }

}
