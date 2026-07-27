using UnityEditor;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [Header("Component Reference")]
    [Tooltip("Firing point, examples: gun barrel tip")]
    public Transform firingPoint;
    [Tooltip("Bullet Prefab")]
    public GameObject bulletPrefab;

    private EnemyController _controller;

    private void Awake()
    {
        _controller = GetComponent<EnemyController>();
    }

    public void OnAttack(Vector2 targetPosition)
    {
        
        if (_controller.enemyData.enemyAttackType == EnemyAttackType.Ranged)
        {
            OnRangedAttack(targetPosition);
            return;
        }

        switch (_controller.enemyData.enemyAttackType)
        {
            case EnemyAttackType.Close:
                OnMeeleAttack();
                break;
            case EnemyAttackType.Kamikaze:
                OnKamikazeAttack();
                break;
        }
    }

    private void OnMeeleAttack()
    {
        Debug.Log($"{_controller.enemyData.enemyName} is performing Meele Attack");
    }

    private void OnRangedAttack(Vector2 targetPosition)
    {
        if (bulletPrefab == null && firingPoint == null) return;
        
        GameObject newBullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        Projectile projectile = newBullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.setup(_controller.enemyData.enemyAttackDamage, targetPosition);
        }
        
        Debug.Log($"{_controller.enemyData.enemyName} is performing Ranged Attack");
    }

    private void OnKamikazeAttack()
    {
        Debug.Log($"{_controller.enemyData.enemyName} is performing Kamikaze Attack");
    }
}
