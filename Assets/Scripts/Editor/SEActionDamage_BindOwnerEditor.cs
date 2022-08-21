using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(SEActionDamage_BindOwner))]
public class SEActionDamage_BindOwnerEditor : SEAction_BaseActionEditor
{
    SEActionDamage_BindOwner Owner;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEActionDamage_BindOwner)target;
        #region 傷害接點名稱
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("傷害接點名稱");
        string socketname = EditorGUILayout.TextField(Owner.SocketName);
        if (socketname != Owner.SocketName)
        {
            Owner.SocketName = socketname;
            EditorUtility.SetDirty(Owner.gameObject);
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        #region 特效產生位置調整

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        Vector3 tmpLocal = EditorGUILayout.Vector3Field("特效產生位置調整", Owner.OffSet);
        if (tmpLocal != Owner.OffSet)
        {
            Owner.OffSet = tmpLocal;
            EditorUtility.SetDirty(Owner.gameObject);
        }


        EditorGUILayout.EndHorizontal();
        #endregion

        #region 特效產生位置旋轉

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        Vector3 tmpLocalRot = EditorGUILayout.Vector3Field("特效產生位置旋轉", Owner.OffRot);
        if (tmpLocalRot != Owner.OffRot)
        {
            Owner.OffRot = tmpLocalRot;
            EditorUtility.SetDirty(Owner.gameObject);
        }


        EditorGUILayout.EndHorizontal();
        #endregion
    }
}
