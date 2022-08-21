using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AttTypeDefine;
[ExecuteInEditMode]
[CustomEditor(typeof(SEAction_SkillInfo))]
public class SEAction_SkillInfoEditor : SEAction_BaseActionEditor
{
    private SEAction_SkillInfo Owner;

    string[] options2 = new string[] { "�S�ĸj�w�@��", "�S�ĸj�w�ۤv","�ˮ`�j�w�ۤv" };

    private void Awake()
    {
        Owner = (SEAction_SkillInfo)target;
        Owner.ObjName = "";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Owner = (SEAction_SkillInfo)target;

        #region ��J�C����H�������M�W��
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(50));

        EditorGUILayout.LabelField("�s�ظ}���j�w����");

        int SkillBindType = EditorGUILayout.Popup((int)Owner.SkillBindType,options2);
        if(SkillBindType != (int)Owner.SkillBindType)
        {
            Owner.SkillBindType = (eSkillBindType)SkillBindType;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        EditorGUILayout.EndHorizontal();

        rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(30));
        EditorGUILayout.LabelField("�s�ظ}���W��");
        string ObjName = EditorGUILayout.TextField(Owner.ObjName);
        if(false == Owner.ObjName.Equals(ObjName))
        {
            Owner.ObjName = ObjName;
            EditorUtility.SetDirty(Owner.gameObject);
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        #region �T�{�ץ�
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("�ЫعC����H"))
        {
            GameObject obj = new GameObject(Owner.ObjName);
            obj.transform.parent = Owner.gameObject.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            obj.AddComponent<SEAction_DataStore>();

            switch (Owner.SkillBindType)
            {
                case eSkillBindType.eEffectOwner:
                    {
                        break;
                    }
                case eSkillBindType.eEffectWorld:
                    {
                        obj.AddComponent<SEAction_SpawnWorld>();
                        break;
                    }
                case eSkillBindType.eDamageOwner:
                    {
                        obj.AddComponent<SEActionDamage_BindOwner>();
                        obj.AddComponent<SEAction_Destruction>();
                        var bc = obj.AddComponent<BoxCollider>();
                        bc.isTrigger = true;
                        bc.enabled = false;
                        break;
                    }
            }

        }

        EditorGUILayout.EndHorizontal();
        #endregion
    }
}
