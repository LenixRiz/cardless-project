using UnityEngine;
using UnityEngine.Pool;

//ON PROGRESS

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [Header("Projectile Pool Settings")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _defaultPoolSize = 20;
    [SerializeField] private int _maxPoolSize = 50;

    private ObjectPool<GameObject> _pool;

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

        _pool = new ObjectPool<GameObject>
        (
            createFunc: CreateProjectile,
            actionOnGet: OnGetProjectile,
            actionOnRelease: OnReleaseProjectile,
            actionOnDestroy: OnDestroyProjectile,
            collectionCheck: true,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize
        );
    }

    private GameObject CreateProjectile()
    {
        GameObject projectile = Instantiate( _projectilePrefab );

        return projectile;
    }

    private void OnGetProjectile(GameObject projectile)
    {
        projectile.SetActive( true );
    }

    private void OnReleaseProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
    }

    private void OnDestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }

    public GameObject GetProjectile()
    {
        return _pool.Get();
    }
}
