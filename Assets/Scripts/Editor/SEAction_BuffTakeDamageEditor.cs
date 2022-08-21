using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AttTypeDefine;
[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_BuffTakeDamage))]
public class SEAction_BuffTakeDamageEditor : SEAction_BaseActionEditor
{
    private SEAction_BuffTakeDamage Owner;

    string[] injureAnimNames = new string[] { "待機","追擊","攻擊","受傷","擊飛","死亡" };

    private void Awake()
    {
        Owner = (SEAction_BuffTakeDamage)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_BuffTakeDamage)target;

        //show 動畫種類;

        #region 播放動畫類型
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        var rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

        EditorGUILayout.LabelField("觸發方式");
        int condition = EditorGUILayout.Popup((int)Owner.AnimID, injureAnimNames);
        if (condition != (int)Owner.AnimID)
        {
            Owner.AnimID = (eStateID)condition;
            EditorUtility.SetDirty(Owner.gameObject);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }
}
