using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Chase, Attack }

    [Header("Current State")]
    public EnemyState currentState = EnemyState.Idle;

    [Header("Detection Range")]
    [SerializeField] private float _detectionRange = 20f;

    private EnemyController _controller;
    private Transform _playerTarget;
    private Rigidbody2D _rb;

    private float _distanceToPlayerTarget;

    private void Start()
    {
        _controller = GetComponent<EnemyController>();
        _rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null )
        {
            _playerTarget = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_controller.enemyData == null|| _playerTarget == null) return;

        _distanceToPlayerTarget = Vector2.Distance(transform.position, _playerTarget.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                IdleLogic();
                break;
            case EnemyState.Chase:
                ChaseLogic();
                break;
            case EnemyState.Attack:
                AttackLogic();
                break;
        }
    }
    private void IdleLogic()
    {
        _rb.linearVelocity = Vector2.zero;

        if (_distanceToPlayerTarget <= _detectionRange)
        {
            if (_controller.enemyData.enemyMovementType == EnemyMovementType.Mobile)
            {
                currentState = EnemyState.Chase;
            }
            else if (_controller.enemyData.enemyMovementType == EnemyMovementType.Static)
            {
                if (_distanceToPlayerTarget <= _controller.enemyData.enemyAttackRange)
                {
                    currentState = EnemyState.Attack;
                }
            }
        }
    }

    private void ChaseLogic()
    {
        if (_distanceToPlayerTarget > _detectionRange)
        {
            currentState = EnemyState.Idle;
            return;
        }

        if (_distanceToPlayerTarget <= _controller.enemyData.enemyAttackRange)
        {
            currentState = EnemyState.Attack;
            return;
        }

        //Logika kejar
        Vector2 direction = (_playerTarget.position - transform.position).normalized;
        _rb.linearVelocity = direction * _controller.CurrentSpeed;
    }

    private void AttackLogic()
    {
        _rb.linearVelocity = Vector2.zero;

        if (_distanceToPlayerTarget > _controller.enemyData.enemyAttackRange)
        {
            if (_controller.enemyData.enemyMovementType == EnemyMovementType.Mobile)
            {
                currentState = EnemyState.Chase;
                
            }
            else
            {
                currentState = EnemyState.Idle;
            }
            return;
        }

        Debug.Log($"{_controller.enemyData.enemyName} is attacking {PlayerManager.Instance.PlayerName}");
    }

}
