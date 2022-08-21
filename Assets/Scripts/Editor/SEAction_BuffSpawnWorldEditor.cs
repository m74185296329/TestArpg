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

        #region �S�Ĺ�@��H

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("�S�Ĺ�@��H");

        var tmpEffect = EditorGUILayout.ObjectField((Object)Owner.EffectSpawnInst, typeof(GameObject), false) as GameObject;

        if (tmpEffect != Owner.EffectSpawnInst)
        {
            Owner.EffectSpawnInst = tmpEffect;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        EditorGUILayout.EndHorizontal();
        #endregion


        #region �S�īO�d�ɶ�
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        float delayTime = EditorGUILayout.FloatField("�S�īO�d�ɶ�", Owner.EffectDestroyDelay);

        if (delayTime != Owner.EffectDestroyDelay)
        {
            Owner.EffectDestroyDelay = delayTime;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion

        #region �S�Ĥj�p
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        float EffectScale = EditorGUILayout.FloatField("�S�Ĥj�p", Owner.EffectScale);

        if (EffectScale != Owner.EffectScale)
        {
            Owner.EffectScale = EffectScale;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion

    }
}
