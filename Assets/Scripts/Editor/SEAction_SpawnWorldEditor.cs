using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_SpawnWorld))]
public class SEAction_SpawnWorldEditor : SEAction_BaseActionEditor
{
    SEAction_SpawnWorld Owner;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_SpawnWorld)target;

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

        #region �S�ı��I�W��
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("�S�ı��I�W��");
        string socketname = EditorGUILayout.TextField(Owner.SocketName);
        if(socketname != Owner.SocketName)
        {
            Owner.SocketName = socketname;
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

        #region �S�Ĳ��ͦ�m�վ�

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        Vector3 tmpLocal = EditorGUILayout.Vector3Field("�S�Ĳ��ͦ�m�վ�", Owner.OffSet);
        if(tmpLocal!= Owner.OffSet)
        {
            Owner.OffSet = tmpLocal;
            EditorUtility.SetDirty(Owner.gameObject);
        }


        EditorGUILayout.EndHorizontal();
        #endregion

        #region �S�Ĳ��ͦ�m����

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        Vector3 tmpLocalRot = EditorGUILayout.Vector3Field("�S�Ĳ��ͦ�m����", Owner.OffRot);
        if (tmpLocalRot != Owner.OffRot)
        {
            Owner.OffRot = tmpLocalRot;
            EditorUtility.SetDirty(Owner.gameObject);
        }


        EditorGUILayout.EndHorizontal();
        #endregion
    }
}
