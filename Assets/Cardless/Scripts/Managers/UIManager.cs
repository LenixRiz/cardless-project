using UnityEngine;

public class UIManager : MonoBehaviour
{
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
