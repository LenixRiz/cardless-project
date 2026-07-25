using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Clip")]
    [SerializeField] private AudioClip deathSFX;

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
    }

    private void OnEnable() 
    {
        PlayerManager.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        sfxSource.PlayOneShot(deathSFX);    
        Debug.Log($"[{this.GetType().Name}] Playing death sound effect.");
    }
}
