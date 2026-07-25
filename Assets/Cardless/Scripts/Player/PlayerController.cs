using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        _playerHealth.isDie += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        _playerHealth.isDie -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath(bool isDied)
    {
        OnPlayerDeath?.Invoke();
        Debug.Log("Player has died.");
    }
}
