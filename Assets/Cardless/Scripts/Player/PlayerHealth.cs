using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public event Action<bool> isDie; 

    private Animator _animator;

    [Header("Attribute")]
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    private void Awake()
    {
        if (_animator == null)
        {
            Debug.LogWarning("Animator component is not assigned.");
        }
        else
        {
            _animator = GetComponent<Animator>();       
        }
        _currentHealth = _maxHealth;
    }

    private void TakeDamage(float damage, string name)
    {
        _currentHealth -= damage;
        _animator?.SetTrigger("isHurt");
        Debug.Log($"Player took {damage} damage from {name}! Current HP: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        _animator?.SetTrigger("isDead");
        isDie?.Invoke(true);
        Debug.Log("Player has died.");
    }

}
