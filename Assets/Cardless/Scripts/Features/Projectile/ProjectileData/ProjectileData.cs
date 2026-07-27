using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "GameData/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public Sprite sprite;

    [Header("Projectile Data")]
    public string projectileName;
    [TextArea(2,5)]public string description;
    [Header("Projectile Attribute")]
    public float projectileSpeed;
}
    