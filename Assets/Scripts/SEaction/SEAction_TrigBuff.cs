    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEAction_TrigBuff : SEAction_BaseAction
{
    private string ConstBuffPath = "Buffs/";
    public string BuffID;
    public override void TrigAction()
    {

        var ae = GetDataStore();

        //��ҤƼW�q
        //var path = ConstBuffPath + BuffID.ToString();

        var path = GlobalHelper.CombineString(ConstBuffPath, BuffID);

        var obj = Resources.Load(path);

        var buffInst = Instantiate(obj) as GameObject;

        //SEAction_BuffInfo

        //�ݭn���D�֬O�����̽֬O������
        var buffInfo = buffInst.GetComponent<SEAction_BuffInfo>();


        //������ : �ϥΧޯ઺�H
        //������ : �ޯ�I�쪺�H
        buffInfo.SetOwner(ae.Owner,ae.Target);

    }
}
