using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AttTypeDefine;

[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_BaseAction))]
public class SEAction_BaseActionEditor : Editor
{
    string[] options = new string[] { "�۰�Ĳ�o", "����Ĳ�o" };
    private SEAction_BaseAction Owner;



    Rect rect;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Owner = (SEAction_BaseAction)target;
        #region Ĳ�o�覡: �۰�or ����
        EditorGUILayout.Space();
        rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

        EditorGUILayout.LabelField("Ĳ�o�覡");
        int condition = EditorGUILayout.Popup((int)Owner.TrigType,options);
        if(condition != (int)Owner.TrigType)
        {
            Owner.TrigType = (eTrigType)condition;
            EditorUtility.SetDirty(Owner.gameObject);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region Ĳ�o����
        EditorGUILayout.Space();

        float delayTime = EditorGUILayout.FloatField("����ɶ�����",Owner.Duration);

        if(delayTime != Owner.Duration)
        {
            Owner.Duration = delayTime;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        #endregion
    }
}
