using System.Text;
using UnityEngine;
using BansheeGz.BGDatabase;
using System.Collections.Generic;

public class GlobalHelper
{
   public static GameObject FindGOByName(GameObject target,string targetName)
    {
        if(null == target)
        {
            return null;
        }

        GameObject resultGO = null;

        if(target.name.Equals(targetName)==true)
        {

        }

        for(var i = 0; i < target.transform.childCount; i++)
        {
            var child = target.transform.GetChild(i).gameObject;
            if (child.name.Equals(targetName) == true)
            {
                return child;
            }
            else
            {
                if (child.transform.childCount > 0)
                {
                    resultGO = FindGOByName(child, targetName);
                    if(null!=resultGO)
                    {
                        return resultGO;
                    }
                }
            }
        }

        return null;
    }


   public static GameObject InstantiateMyPrefab(string path,Vector3 pos,Quaternion rot)
    {
        var obj = Resources.Load(path);

        var go = Object.Instantiate(obj) as GameObject;
        go.name = obj.name;

        go.transform.position = pos;
        go.transform.rotation = rot;
        go.transform.localScale = Vector3.one;
        return go;
    }

    public static string CombineString (string a,string b)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(a);
        sb.Append(b);
        return sb.ToString();
    }

    public static Transform GetNearestTrans(List<Transform> list, Transform target)
    {
        if (list.Count == 0 || target == null)
            return null;
        float MinDis = float.MaxValue;
        int TargetIndex = 0;
        for(var i = 0; i < list.Count; i++)
        {
            var dis = Vector3.Distance(list[i].position, target.position);
            if (dis < MinDis)
            {
                MinDis = dis;
                TargetIndex = i;
            }
        }

        return list[TargetIndex];
    }

    #region table sheets

    public static T GetTheEntityByName<T>(string tableName, string name) where T : BGEntity
    {

        BGMetaEntity table = BGRepo.I[tableName];

        List<BGEntity> result = table.FindEntities(
                entity => !string.IsNullOrEmpty(entity.Name) && entity.Name == name) as List<BGEntity>;

        if (result == null || result.Count == 0)
        {
            return null;
        }
        return result[0] as T;
    }
    #endregion
}
