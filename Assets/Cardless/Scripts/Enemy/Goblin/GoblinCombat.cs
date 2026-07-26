using UnityEngine;

public class GoblinCombat : MonoBehaviour
{
    [Header("Goblin Attributes")]
    [SerializeField] private string _enemyName = "Goblin";
    [SerializeField] private float _enemyMaxHealth = 100f;
    [SerializeField] private float _enemyAttackDamage = 15f;
    [SerializeField] private float _enemyExperienceGiven = 15f;

    private float _currentHealth;
    private float _currentAttackDamage;

    private void Awake()
    {
        _currentHealth = _enemyMaxHealth;
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
