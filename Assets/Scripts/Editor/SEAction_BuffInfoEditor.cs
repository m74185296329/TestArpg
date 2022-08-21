using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AttTypeDefine;
[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_BuffInfo))]
public class SEAction_BuffInfoEditor : SEAction_BaseActionEditor
{
    private SEAction_BuffInfo Owner;

    string[] options2 = new string[] { "�S�ĸj�w�@��", "�S�ĸj�w�ۤv","�ˮ`�j�w�ۤv" };

    private void Awake()
    {
        Owner = (SEAction_BuffInfo)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_BuffInfo)target;

    }
}
