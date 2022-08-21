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

    string[] options2 = new string[] { "特效綁定世界", "特效綁定自己","傷害綁定自己" };

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
