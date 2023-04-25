using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthManager))]
public class HealthManagerEditor : Editor
{
    private HealthManager Target { get { return target as HealthManager; } }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
            return;

        var newValue = EditorGUILayout.Slider("Hp", Target.Health / Target.MaxHp, 0, 1);
        Target.SetHealth(newValue * Target.MaxHp);
    }
}
