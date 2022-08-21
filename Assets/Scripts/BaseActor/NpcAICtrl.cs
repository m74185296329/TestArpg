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

                Owner.transform.LookAt(PlayerInst.transform);

                var tmp = Owner.transform.forward;

                tmp.y = 0f;
                Owner.transform.forward = tmp;
                //injure play is over, set state to chase.

            }
            else
            {
                if (value != npcState)
                {
                    if (value != eStateID.eChase)
                    {
                        Owner.Anim.SetFloat("Speed", 0f);
                    }

                    switch (value)
                    {
                        case eStateID.eAttack:
                            {

                                AnimMgr.StartAnimation("Base Layer.Attack1", null, CastSkillBegin, CastSkillEnd, null);
                                break;
                            }
                        case eStateID.eTaunting:
                            {
                                Owner.Anim.SetTrigger("Base Layer.Taunting");
                                break;
                            }
                        case eStateID.eWalkBack:
                            {
                                Owner.Anim.SetTrigger("Base Layer.WalkBack");
                                walkbackStartTime = Time.time;
                                break;
                            }

                    }
                }
            }
            npcState = value;
        }
    }
    void CastSkillBegin()
    {

    }

    void CastSkillEnd()
    {
        if (NpcState == eStateID.eGetHit)
            return;

        NpcState = GetCurNpcAIState(eStateID.eAttack);



        //NpcState = eStateID.eChase;
    }


    bool IsTrigger = false;

    NpcActor Owner;

    BasePlayer PlayerInst;

    float ChaseDis;
    AnimatorManager AnimMgr;

    #region walkback
    public float walkbackSpeed = 1f;
    public float walkbackDuration = 2f;
    public float walkbackStartTime;
    void UpdateWalkBack()
    {
        if(Time.time - walkbackStartTime >= walkbackDuration)
        {
            NpcState = eStateID.eChase;
            Owner.Anim.SetTrigger("Base Layer.Run");
            return;
        }
        else
        {
            transform.DOLookAt(PlayerInst.transform.position,0.2f);

            var tmp = transform.forward;
            tmp.y = 0f;
            transform.forward = tmp;

            transform.position += (-1f) * walkbackSpeed * Time.deltaTime * transform.forward;
        }
    }
    #endregion

    public void OnStart(NpcActor NA)
    {
        Owner = NA;
        IsTrigger = true;
        PlayerInst = Owner.PlayerInst;
        NpcState = eStateID.eChase;
        AnimMgr = gameObject.GetComponent<AnimatorManager>();
        AnimMgr.OnStart(Owner);
        InitPercentage(out dicAttackPercent,out listAttackPercent,AttackTauntPer,AttackChase,AttackWalkback);
        InitPercentage(out dicGethitPercent,out listGethitPercent, GetHitTauntPer, GetHitChase, GetHitWalkback);
    }

    #region percentage calculation
    Dictionary<int, eStateID> dicAttackPercent;
    List<int> listAttackPercent;
    Dictionary<int, eStateID> dicGethitPercent;
    List<int> listGethitPercent;
    void InitPercentage(out Dictionary<int,eStateID> dic,out List<int>list ,int taunt,int chase,int walkback)
    {
        dic = new Dictionary<int, eStateID>();
        dic[taunt] = eStateID.eTaunting;
        dic[chase] = eStateID.eChase;
        dic[walkback] = eStateID.eWalkBack;

        var array = list = new List<int>();

        array.Add(taunt);
        array.Add(chase);
        array.Add(walkback);

        GlobalHelper.QuickSortStrict(array);

        var tmp = dic[array[2]];
        dic.Remove(array[2]);
        dic.Add(100, tmp);

        tmp = dic[array[1]];
        dic.Remove(array[1]);
        dic.Add(array[0] + array[1], tmp);

        array[2] = 100;
        array[1] = array[0] + array[1];
    }

    #endregion
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
            case eStateID.eWalkBack:
                {
                    UpdateWalkBack();
                    break;
                }
        }
    }


    public int AttackChase = 10;
    public int AttackTauntPer = 40;
    public int AttackWalkback = 50;

    public int GetHitChase = 15;
    public int GetHitTauntPer = 1;
    public int GetHitWalkback = 84;
    eStateID GetCurNpcAIState(eStateID id)
    {
        var percentage = Random.Range(0, 100);
        Dictionary<int, eStateID> dicPer;
        List<int> listPer;
        switch (id)
        {
            case eStateID.eAttack:
                {
                    dicPer = dicAttackPercent;
                    listPer = listAttackPercent;
                    break;
                }
            case eStateID.eGetHit:
                {
                    dicPer = dicGethitPercent;
                    listPer = listGethitPercent;
                    break;
                }
            default:
                {
                    return eStateID.eNULL;
                }
        }
        //���v: 



        if (percentage < listPer[0])
        {
            return dicPer[listPer[0]];

        }
        else if (percentage >= listPer[0] && percentage < listPer[1])
        {
            return dicPer[listPer[1]];
        }
        else
        {
            return dicPer[listPer[2]];
        }


        return eStateID.eNULL;
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
                    StartCoroutine(WaitForAWhile());
                    
                    break;
                }
            case eStateID.eTaunting:
                {
                    NpcState = eStateID.eChase;
                    break;
                }
        }
    }

    IEnumerator WaitForAWhile()
    {
        yield return new WaitForSeconds(0.5f);
        NpcState = GetCurNpcAIState(eStateID.eGetHit);
    }


}
