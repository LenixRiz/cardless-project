using UnityEngine;

public class GolemCombat : MonoBehaviour
{
    [Header("Golem Attributes")]
    [SerializeField] private string _enemyName = "Golem";
    [SerializeField] private float _enemyMaxHealth = 200f;
    [SerializeField] private float _enemyAttackDamage = 5f;
    [SerializeField] private float _enemyExperienceGiven = 25f;

    private float _currentMaxHealth;
    private float _currentAttackDamage;

    private void Awake()
    {
        _currentMaxHealth = _enemyMaxHealth;
        _currentAttackDamage = _enemyAttackDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_currentAttackDamage, _enemyName);
        }
    }
}
