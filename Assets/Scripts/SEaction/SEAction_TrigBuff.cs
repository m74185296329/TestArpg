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

        //實例化增益
        //var path = ConstBuffPath + BuffID.ToString();

        var path = GlobalHelper.CombineString(ConstBuffPath, BuffID);

        var obj = Resources.Load(path);

        var buffInst = Instantiate(obj) as GameObject;

        //SEAction_BuffInfo

        //需要知道誰是攻擊者誰是受擊者
        var buffInfo = buffInst.GetComponent<SEAction_BuffInfo>();


        //攻擊者 : 使用技能的人
        //受擊者 : 技能碰到的人
        buffInfo.SetOwner(ae.Owner,ae.Target);

    }
}
