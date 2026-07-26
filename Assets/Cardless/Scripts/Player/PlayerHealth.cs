using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<bool> IsDie; 

    private Animator _animator;

    private string _playerName;
    private float _currentHealth;
    private bool _isDead = false;

    //Invisibility
    private bool _isInvisible = false;
    private float _invisibilityCooldown = 10f;
    private float _invisibilityDuration = 3f;
    private float _nextInvisibilityTime = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            _playerName = PlayerManager.Instance.PlayerName;
            _currentHealth = PlayerManager.Instance.PlayerMaxHealth;
        }
        else
        {
            Debug.LogError("PlayerManager instance is not found.");
        }
    }

    public void TakeDamage(float damage, string name)
    {
        //Check if player is dead or invisible
        if (_isDead || _isInvisible) return;

        //if not dead or invisible take damage
        _currentHealth -= damage;
        _animator?.SetTrigger("isHurt");
        Debug.Log($"{_playerName} took {damage} damage from {name}! Current HP: {_currentHealth}");

        //if health below 0 player dead and stop
        if (_currentHealth <= 0)
        {
            OnDie();
            return;
        }

        //if player damaged or cooldown expired, invisibility active
        if (Time.time >= _nextInvisibilityTime)
        {
            TriggerInvisibility();
        }
    }

    private async void TriggerInvisibility()
    {
        _isInvisible = true;
        _nextInvisibilityTime = Time.time + _invisibilityCooldown;
        Debug.Log($"[{this.GetType().Name}] Invisible for {_invisibilityDuration} seconds, next invisibility at {_nextInvisibilityTime}");

        try
        {
            await Awaitable.WaitForSecondsAsync(_invisibilityDuration);
            RemoveInvisibility();
        }
        catch (OperationCanceledException)
        {
            Debug.Log($"[{GetType().Name}] Invisibility canceled because player is dead");
        }
    }

    private void RemoveInvisibility()
    {
        if (!_isInvisible) return;

        _isInvisible = false;
    }

    private void OnDie()
    {
        _isDead = true;

        _animator?.ResetTrigger("isHurt");
        _animator?.SetTrigger("isDead");

        IsDie?.Invoke(true);
    }

}
