using UnityEngine;

public enum EnemyMovementType
{
    Mobile,
    Static
}

public enum EnemyAttackType
{
    Close,
    Ranged,
    Kamikaze
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Identity Setting")]
    [Tooltip("Write Unique ID")]
    public int enemyId;

    [Tooltip("Give them cool name and humongus description")]
    public string enemyName;
    [TextArea(3,5)]public string enemyDescription;

    [Header("Visual Settings")]
    [Tooltip("Place enemy sprite here")]
    public Sprite sprite;

    [Header("Type")]
    public EnemyMovementType enemyMovementType;
    public EnemyAttackType enemyAttackType;

    [Space(10)]
    [Header("Combat Stats")]
    [Tooltip("Health")]
    public float enemyMaxHealth;
    public float enemyMinHealth;
    [Tooltip("IShowSpeed")]
    public float enemyMaxSpeed;
    public float enemyMinSpeed;
    [Tooltip("How much damage does the enemy produce and their attack speed")]
    public float enemyAttackDamage;
    public float enemyAttackCooldown;
    [Tooltip("How far can enemy ranged attack reach")]
    public float enemyAttackRange;

    private void OnValidate()
    {
        //t: adalah untuk mencari tipe data (in dis case adalah scriptable object), kalau gak pake itu bakal dicari semua yang namanya ada EnemyData
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:EnemyData");

        foreach (string guid in guids)
        {
            //ambil jalur lokasi file berdasarkan guid
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);

            //load filenya ke memori sementara untuk dideteksi
            EnemyData otherEnemy = UnityEditor.AssetDatabase.LoadAssetAtPath<EnemyData>(path);

            if (otherEnemy != null && otherEnemy != this && otherEnemy.enemyId == this.enemyId && otherEnemy.enemyId != 0)
            {
                Debug.LogWarning($"<color=yellow>[DATABASE WARNING]</color> ID <b>{this.enemyId}</b> in {this.name} is the same as {otherEnemy.name} file. Please change the ID");
            }
        }
    }
}
