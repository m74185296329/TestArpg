using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;

public class StateMachine : StateMachineBehaviour
{

    bool IsLastTransition;
    bool IsCurTransition;
    AnimatorStateInfo LastStateInfo;
    Dictionary<eTrigSkillState, List<NotifySkill>> SkillDic = new Dictionary<eTrigSkillState, List<NotifySkill>>();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        IsCurTransition = animator.IsInTransition(layerIndex);

        if (!IsCurTransition)
        {
            if(stateInfo.normalizedTime % 1.0 < LastStateInfo.normalizedTime % 1.0f)
            {
                TrigAction(eTrigSkillState.eTrigEnd);
            }
        }


        if(IsCurTransition&&!IsLastTransition)
        {
            TrigAction(eTrigSkillState.eTrigBegin);
        }

        if(!IsCurTransition && IsLastTransition)
        {
            TrigAction(eTrigSkillState.eTrigEnd);
        }
        IsLastTransition = IsCurTransition;
        LastStateInfo = stateInfo;
    }

    void TrigAction(eTrigSkillState state)
    {
        if (SkillDic.ContainsKey(state))
        {
            var list = SkillDic[state];
            while (list.Count > 0)
            {
                var ns = list[0];
                list.Remove(ns);
                ns();
            }
        }
    }

    public void RegisterCallback(eTrigSkillState state,NotifySkill action)
    {
        List<NotifySkill> list;
        if (SkillDic.ContainsKey(state))
        {
            list = SkillDic[state];
            list.Add(action);
        }
        else
        {
            list = new List<NotifySkill>();
            list.Add(action);
            SkillDic.Add(state, list);
        }
    }
    public void ClearAllCallback()
    {
        if (null == SkillDic)
            return;

        List<NotifySkill> list;
        for(var i= eTrigSkillState.eTrigBegin;i<= eTrigSkillState.eTrigEnd; i++)
        {
            if (SkillDic.ContainsKey(i))
            {
                list = SkillDic[i];
                list.Clear();
            }
        }
    }
}
