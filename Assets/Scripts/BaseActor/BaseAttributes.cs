using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
using com.dxz.config;

public class BaseAttributes : MonoBehaviour
{


    private float speed;
    public float Speed => (speed);

    private float attackdis;
    public float AttackDis => (attackdis);

    //hp,attack
    int[] attrs;

    BGE_PlayerTemplate PlayerTpl;

    BGE_PlayerAttTemplate PlayerAttTpl;

    BasePlayer Owner;
    void Awake()
    {
        attrs = new int[(int)ePlayerAttr.eSize];
    }


    //建立表格 done

    //填寫表格數據 done

    //讀取表格數據

    //將表格數據復職給BaseAttributes的成員變量

    //初始化角色的信息
    public void InitPlayerAttr(BasePlayer bp ,string Name)
    {
        PlayerTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerTemplate>("PlayerTemplate", Name);
        PlayerAttTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerAttTemplate>("PlayerAttTemplate", Name);

        Owner = bp;
        this[ePlayerAttr.eMaxHP] = PlayerAttTpl.f_MAXHP;
        this[ePlayerAttr.eAttack] = PlayerAttTpl.f_Attack;
        this[ePlayerAttr.eHP] = PlayerAttTpl.f_HP;
        speed = PlayerAttTpl.f_Speed;
        attackdis = PlayerAttTpl.f_AttackDis;
    }
    public int this[ePlayerAttr att]
    {
        get
        {

            if(att <= ePlayerAttr.eNULL)
            {
                return -1;
            }
            else
            {
                return attrs[(int)att];
            }

        }
        set
        {
            if(att<= ePlayerAttr.eNULL)
            {
                Debug.LogError("Logic Error:" + att);
                return;
            }

            if (value != attrs[(int)att])
            {
                
                if(att == ePlayerAttr.eHP && Owner.PlayerSide == ePlayerSide.eEnemy)
                {
                    if (attrs[(int)att] == 0 && value == this[ePlayerAttr.eMaxHP])
                    {
                        attrs[(int)att] = value;
                        return;
                    }

                    attrs[(int)att] = value;
                    //update player hp

                    float cur = (float)attrs[(int)att];

                    var hpPer = cur / this[ePlayerAttr.eMaxHP];
                    ((NpcActor)Owner).UpdateHp(hpPer);
                }
                else
                {
                    attrs[(int)att] = value;
                }
            }
        }
    }
}
