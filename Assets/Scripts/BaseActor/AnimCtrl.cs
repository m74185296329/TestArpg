using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimCtrl : MonoBehaviour
{
    #region Sys funcs
    public Vector2[] AnimPerArray;
    public UI_JoyStick JoyStickInst;


    AnimatorManager AnimMgr;
    Animator _Anim;
    int _CurAnimAttackIndex = 1;
    int MinAnimAttackIndex = 1;
    int MaxAnimAttackIndex = 3;
    string CurAnimName;
    string AttackPre = "Base Layer.Atk";
    bool IsReady = true;

    EmmaSword WeaponInst;


    bool _IsPlaying;
    public bool IsPlaying =>(_IsPlaying);
    public Animator Anim => (_Anim);
    private void Awake()
    {
        AnimMgr = gameObject.AddComponent<AnimatorManager>();

    }
    private void Start()
    {
        _Anim = GetComponent<Animator>();
        AnimMgr.OnStart(this);

        var weapongo = GlobalHelper.FindGOByName(gameObject, "greatesword");
        if (null != weapongo)
        {
            WeaponInst = weapongo.GetComponent<EmmaSword>();
            WeaponInst.OnStart(this);
        }

        JoyStickInst.FinalSkillBtnInst.PressDown.AddListener((a) => OnFinalSkillBegin(a));
        JoyStickInst.FinalSkillBtnInst.OnDragEvent.AddListener((a) => OnFinalSkillDrag(a));
        JoyStickInst.FinalSkillBtnInst.PressUp.AddListener((a) => OnFinalSkillEnd(a));


    }
    private void Update()
    {
        UpdateSkillInput();
    }
    #endregion

    #region Cast Attack

    void UpdateSkillInput()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            CastSkill();
        }
    }
#endif
    void CastSkill()
    {
        if (!IsReady)
            return;
        if(_CurAnimAttackIndex > MaxAnimAttackIndex)
        {
            _CurAnimAttackIndex = MinAnimAttackIndex;
        }
        CurAnimName = AttackPre + _CurAnimAttackIndex.ToString();
        AnimMgr.StartAnimation(CurAnimName,CastSkillReady, CastSkillBegin,CastSkillEnd,CastSkillEnd1);
    }

    void CastSkillReady()
    {
        IsReady = true;
    }

    void CastSkillBegin()
    {
        _IsPlaying = true;

        IsReady = false;

        _CurAnimAttackIndex++;
    }
    #endregion

    void CastSkillEnd1()
    {
        if(_CurAnimAttackIndex <= 1)
        {
            Debug.LogError("Logic Error");
            return;
        }
        var item = AnimPerArray[_CurAnimAttackIndex - 2];

        WeaponInst.OnStartWeaponCtrl(Anim,item.x,item.y);
    }

    void CastSkillEnd()
    {
        _CurAnimAttackIndex = MinAnimAttackIndex;

        IsReady = true;
        _IsPlaying = false;
    }

    #region Final skill
    public void OnModifyFSV()
    {
        //increase slider ->UI
        JoyStickInst.OnModifyFSV();
    }

    public void OnFinalSkillBegin(PointerEventData data)
    {

    }

    public void OnFinalSkillDrag(PointerEventData data)
    {

    }

    public void OnFinalSkillEnd(PointerEventData data)
    {

    }

    #endregion
}
