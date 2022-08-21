using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
public class SEAction_BuffTakeDamage : SEAction_BaseAction
{
    [HideInInspector]
    public eStateID AnimID;
    public override void TrigAction()
    {
        var ds = GetDataStore();

        var attacker = ds.Owner.GetComponent<BasePlayer>();

        var defencer = ds.Target.GetComponent<BasePlayer>();

        //1:hp
        //2:attack
        var hp = defencer.BaseAttr[ePlayerAttr.eHP];

        var attack = attacker.BaseAttr[ePlayerAttr.eAttack];
        
        if(attacker.PlayerSide == ePlayerSide.ePlayer)
        {
            ((AnimCtrl)attacker).OnModifyFSV(25);
        }

        hp -= attack;


        if (hp <= 0)
        {
            defencer.BaseAttr[ePlayerAttr.eHP] = 0;

            if (defencer.PlayerSide == ePlayerSide.eEnemy)
            {
                ((NpcActor)defencer).SetAIState(eStateID.eDie);
                ((AnimCtrl)attacker).EnemyDie(defencer.transform);
            }
            
        }
        else
        {
            defencer.BaseAttr[ePlayerAttr.eHP] = hp;

            //播放受傷動畫
            switch (AnimID)
            {
                case eStateID.eGetHit:
                    {

                        if(defencer.PlayerSide == ePlayerSide.eEnemy)
                        {
                            ((NpcActor)defencer).SetAIState(eStateID.eGetHit);
                        }

                        //defencer.PlayAnim("Base Layer.GetHit");


                        break;
                    }
                case eStateID.eFlyAway:
                    {
                        if (defencer.PlayerSide == ePlayerSide.eEnemy)
                        {

                        }
                        break;
                    }
            }
        }

    }
}
