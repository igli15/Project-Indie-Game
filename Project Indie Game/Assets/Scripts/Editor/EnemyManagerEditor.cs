using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyManager))]
public class EnemyManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //EditorGUILayout.Space();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EnemyManager enemyManager = (EnemyManager)target;

        enemyManager.numWave=EditorGUILayout.IntField("Number of waves", enemyManager.numWave);
        if (enemyManager.numWave > 200) enemyManager.numWave = 200;

        //
        for (int i = 0; i < enemyManager.spawners.Count; i++)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.BeginHorizontal();
            for (int waveIndex = 0; waveIndex < enemyManager.numWave; waveIndex++)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.IntField("Goomba", 1, GUILayout.MaxWidth(100.0f));
                EditorGUILayout.IntField("Turret", 1, GUILayout.MaxWidth(100.0f));
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

    }
}
