using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sound Effect")]
    [SerializeField] private AudioClip deathSoundEffect;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Playing death sound effect.");
    }
}
