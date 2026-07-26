using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyData data = target as EnemyData;
        serializedObject.Update();

        //Untuk exclude yang ingin ditampilkan agar tidak digambar dulu di inspector
        DrawPropertiesExcluding(serializedObject, 
            "enemyId", "enemyName", "enemyDescription", "enemyMovementType", "enemyAttackType", "enemyMaxHealth", "enemyMinHealth", 
            "enemyMaxSpeed", "enemyMinSpeed", "enemyAttackDamage", "enemyAttackRange", "enemyAttackSpeed");

        //ditaruh diluar agar tetap keliatan dalam kondisi apapun
        SerializedProperty attackTypeProp = serializedObject.FindProperty("enemyAttackType");

        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyId"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyDescription"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyMovementType"));

        //ATTACK TYPE ONLY RANGED ON STATIC
        if (data.enemyMovementType == EnemyMovementType.Static)
        {
            //Paksa jadi ranged
            attackTypeProp.enumValueIndex = (int)EnemyAttackType.Ranged;

            //disable UI agar tidak bisa diganti
            GUI.enabled = false;
            EditorGUILayout.PropertyField(attackTypeProp, new GUIContent("Enemy Attack Type Locked to Ranged"));
            GUI.enabled = true;
        }

        if (data.enemyAttackType == EnemyAttackType.Ranged)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Attack Range Settings", EditorStyles.boldLabel);

            //Ambil prperty dari serializedObject
            SerializedProperty attackRangeProp = serializedObject.FindProperty("enemyAttackRange");

            EditorGUILayout.PropertyField(attackRangeProp);
        }

        if (data.enemyMovementType == EnemyMovementType.Mobile)
        {

            EditorGUILayout.PropertyField(attackTypeProp);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mobile Movement Settings", EditorStyles.boldLabel);

            //Tampilkan kolom input speed
            SerializedProperty maxSpeedProp = serializedObject.FindProperty("enemyMaxSpeed");
            SerializedProperty minSpeedProp = serializedObject.FindProperty("enemyMinSpeed");

            EditorGUILayout.PropertyField(maxSpeedProp);
            EditorGUILayout.PropertyField (minSpeedProp);
        }

        //EditorGUILayout.LabelField("CombatStats", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyMaxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyMinHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyAttackDamage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyAttackSpeed"));

        serializedObject.ApplyModifiedProperties();

    }
}
