using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Input Data")]
    public ProjectileData projectileData;

    [Header("Internal Reference")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;

    private float _projectileSpeed;
    private float _damage;
    private Vector2 _direction;
    private Transform _projectileSource;

    public void setup(float damage, Vector2 targetPosition)
    {
        _projectileSpeed = projectileData.projectileSpeed;
        _damage = damage;

        _direction = (targetPosition - (Vector2)_projectileSource.position).normalized;

        if (projectileData == null)
        {
            Debug.LogWarning("Projectile Data not found!");
            return;
        }

        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = projectileData.sprite;
        }

        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction * _projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var targetDamageable))
        {
            targetDamageable.TakeDamage(_damage, projectileData.projectileName);

            Destroy(gameObject);
        }
    }
}
