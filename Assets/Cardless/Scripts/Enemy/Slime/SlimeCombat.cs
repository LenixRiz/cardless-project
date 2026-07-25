using System;
using UnityEngine;

public class SlimeCombat : MonoBehaviour
{
    public static event Action<float, string> OnEnemyAttack; // Event to notify when the enemy attacks

    [Header("Attribute")]
    [SerializeField] private string _enemyName = "Green Slime";
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _attackDamage = 15f;

    private float _currentHealth;
    private float _currentAttackDamage;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _currentAttackDamage = _attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            OnEnemyAttack?.Invoke(_currentAttackDamage, _enemyName);
        }
    }
}
