using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Destroy(gameObject);
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
        Debug.Log($"[{this.GetType().Name}] Updating Player Death UI");
    }
}
