using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
using DG.Tweening;
public class NpcAICtrl : MonoBehaviour
{

    eStateID npcState = eStateID.eNULL;
    public eStateID NpcState
    {
        get
        {
            return npcState;
        }
        set
        {
            if (value == eStateID.eGetHit)
            {
                Owner.Anim.SetFloat("Speed", 0f);
                // play injure animation
                Owner.Anim.SetTrigger("Base Layer.GetHit");
                //injure play is over, set state to chase.

            }
            else
            {
                if(value != npcState)
                {
                    Owner.Anim.SetFloat("Speed", 0f);
                }
            }
            npcState = value;
        }
    }

    bool IsTrigger = false;

    NpcActor Owner;

    BasePlayer PlayerInst;

    float ChaseDis;
    public void OnStart(NpcActor NA)
    {
        Owner = NA;
        IsTrigger = true;
        PlayerInst = Owner.PlayerInst;
        NpcState = eStateID.eChase;
    }
    private void Update()
    {
        if (!IsTrigger)
            return;
        switch (NpcState)
        {
            case eStateID.eChase:
                {
                    //�P�_��̶Z��,�p������Z���N����
                    ChaseDis = Vector3.Distance(transform.position, PlayerInst.transform.position);
                    if(ChaseDis < (PlayerInst.PlayerRadius + Owner.PlayerRadius)*Owner.BaseAttr.AttackDis)
                    {
                        NpcState = eStateID.eAttack;
                        return;
                    }
                    //�µ۪��a�i��

                    //�¦V
                    transform.DOLookAt(PlayerInst.transform.position, 0.1f);
                    //�l���t��
                    transform.position += transform.forward * Owner.BaseAttr.Speed * Time.deltaTime;
                    //����l���ʵe
                    Owner.Anim.SetFloat("Speed", 1f);

                    break;
                }
        }
    }

    void EventAnimBegin()
    {

    }

    void EventAnimEnd(int id)
    {
        eStateID ID = (eStateID)id;

        switch (ID)
        {
            case eStateID.eGetHit:
                {
                    NpcState = eStateID.eChase;
                    break;
                }
        }
    }

}
