using Unity.VisualScripting;
using UnityEngine;

public class SlimeCombat : MonoBehaviour
{
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
        Debug.Log($"{_enemyName} collided with {other.gameObject.name}");

        if (other.gameObject.name == "Player")
        {
            Debug.Log($"{_enemyName} is attacking the player for {_currentAttackDamage} damage!");
        }
    }
}
