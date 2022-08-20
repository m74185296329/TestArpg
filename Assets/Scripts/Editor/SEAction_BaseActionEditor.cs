using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AttTypeDefine;

[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_BaseAction))]
public class SEAction_BaseActionEditor : Editor
{
    string[] options = new string[] { "自動觸發", "條件觸發" };
    private SEAction_BaseAction Owner;



    Rect rect;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Owner = (SEAction_BaseAction)target;
        #region 觸發方式: 自動or 條件
        EditorGUILayout.Space();
        rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

        EditorGUILayout.LabelField("觸發方式");
        int condition = EditorGUILayout.Popup((int)Owner.TrigType,options);
        if(condition != (int)Owner.TrigType)
        {
            Owner.TrigType = (eTrigType)condition;
            EditorUtility.SetDirty(Owner.gameObject);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region 觸發延遲
        EditorGUILayout.Space();

        float delayTime = EditorGUILayout.FloatField("延遲時間長度",Owner.Duration);

        if(delayTime != Owner.Duration)
        {
            Owner.Duration = delayTime;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion
    }
}
