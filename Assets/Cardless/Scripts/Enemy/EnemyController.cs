using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Input Data")]
    public EnemyData enemyData;

    [Header("Internal Ref")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;

    private float _currentHealth;
    private float _currentSpeed;
    private float _currentAttackDamage;
    private float _currentAttackCooldown;
    private float _currentAttackRange;

    public float CurrentHealth => _currentHealth;
    public float CurrentSpeed => _currentSpeed;

    private void Start()
    {
        SetupEnemy();
    }

    private void SetupEnemy()
    {
        if (enemyData == null)
        {
            Debug.LogError("No Enemy Data found!");
            return;
        }

        if (enemyData.sprite != null)
        {
            _spriteRenderer.sprite = enemyData.sprite;
        }

        SetupAttribute();

        SetupMovementBehavior();
    }

    private void SetupAttribute()
    {
        _currentHealth = Random.Range(enemyData.enemyMinHealth, enemyData.enemyMaxHealth);
        _currentSpeed = Random.Range(enemyData.enemyMinSpeed, enemyData.enemyMaxSpeed);
        _currentAttackDamage = enemyData.enemyAttackDamage;
        _currentAttackCooldown = enemyData.enemyAttackCooldown;
        _currentAttackRange = enemyData.enemyAttackRange;
    }

    private void SetupMovementBehavior()
    {
        switch (enemyData.enemyMovementType)
        {
            case EnemyMovementType.Mobile:
                _rb.bodyType = RigidbodyType2D.Dynamic;
                break;
            case EnemyMovementType.Static:
                _rb.bodyType = RigidbodyType2D.Kinematic;
                _rb.linearVelocity = Vector2.zero;
                break;
        }
    }
}
