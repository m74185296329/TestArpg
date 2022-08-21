using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_BuffSpawnWorld))]
public class SEAction_BuffSpawnWorldEditor : SEAction_BaseActionEditor
{
    SEAction_BuffSpawnWorld Owner;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_BuffSpawnWorld)target;

        #region 特效實作對象

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("特效實作對象");

        var tmpEffect = EditorGUILayout.ObjectField((Object)Owner.EffectSpawnInst, typeof(GameObject), false) as GameObject;

        if (tmpEffect != Owner.EffectSpawnInst)
        {
            Owner.EffectSpawnInst = tmpEffect;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        EditorGUILayout.EndHorizontal();
        #endregion


        #region 特效保留時間
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        float delayTime = EditorGUILayout.FloatField("特效保留時間", Owner.EffectDestroyDelay);

        if (delayTime != Owner.EffectDestroyDelay)
        {
            Owner.EffectDestroyDelay = delayTime;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion

        #region 特效大小
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        float EffectScale = EditorGUILayout.FloatField("特效大小", Owner.EffectScale);

        if (EffectScale != Owner.EffectScale)
        {
            Owner.EffectScale = EffectScale;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion

    }
}
