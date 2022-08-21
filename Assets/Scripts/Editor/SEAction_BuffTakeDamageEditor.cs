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

    string[] injureAnimNames = new string[] { "�ݾ�","�l��","����","����","����","���`" };

    private void Awake()
    {
        Owner = (SEAction_BuffTakeDamage)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_BuffTakeDamage)target;

        //show �ʵe����;

        #region ����ʵe����
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        var rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

        EditorGUILayout.LabelField("Ĳ�o�覡");
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
