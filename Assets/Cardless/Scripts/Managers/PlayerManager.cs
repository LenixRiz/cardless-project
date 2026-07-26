using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public static event Action OnPlayerDeath;

    [Header("Player Attributes")]
    [SerializeField] private string _playerName = "Player";
    [SerializeField] private float _playerMaxHealth = 100f;
    [SerializeField] private float _playerAttackDamage = 20f;
    [SerializeField] private float _playerExperience = 0f;

    public string PlayerName => _playerName;
    public float PlayerMaxHealth => _playerMaxHealth;
    public float PlayerAttackDamage => _playerAttackDamage;
    public float PlayerExperience => _playerExperience;

    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        _playerHealth.IsDie += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        _playerHealth.IsDie -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath(bool isDied)
    {
        _playerMovement.StopMovement();
        _playerMovement.enabled = false;
        OnPlayerDeath?.Invoke();
        Debug.Log($"[{this.GetType().Name}] Player has died.");
    }
}
