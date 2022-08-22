using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AttTypeDefine;
using DG.Tweening;
using com.dxz.config;

public class AnimCtrl : BasePlayer
{
    #region Sys funcs

    List<Transform> EnemyList;

    public UI_JoyStick JoyStickInst;

    FinalSkillBtn FinalSkillInst;


    int _CurAnimAttackIndex = 1;
    int MinAnimAttackIndex = 1;
    int MaxAnimAttackIndex = 3;
    string CurAnimName;
    string AttackPre = "Base Layer.Atk";
    string SkillPre = "Base Layer.Skill";
    string SkillPrePath = "Skills/";
    bool IsReady = true;

    Camera Cam;

    //EmmaSword WeaponInst;


    bool _IsPlaying;
    public bool IsPlaying => (_IsPlaying);

    eSkillType SkillType;

 

    SEAction_SkillInfo SkillInfo;

    Movementinput MoveInput;

    protected override void Awake()
    {
        base.Awake();

        EnemyList = new List<Transform>();


    }
    protected override void Start()
    {
        base.Start();

        Anim.runtimeAnimatorController = Instantiate(Resources.Load("AnimatorController/"+PlayerTpl.f_AnimCtrlPath)) as RuntimeAnimatorController;

        AnimMgr.OnStart(this);

        FinalSkillInst = JoyStickInst.FinalSkillBtnInst;

        Cam = Camera.main;


        JoyStickInst.FinalSkillBtnInst.PressDown.AddListener((a) => OnFinalSkillBegin(a));
        JoyStickInst.FinalSkillBtnInst.OnDragEvent.AddListener((a) => OnFinalSkillDrag(a));
        JoyStickInst.FinalSkillBtnInst.PressUp.AddListener((a) => OnFinalSkillEnd(a));

        LoadFinalSkillArrow();

    }
    private void Update()
    {
        
        UpdateSkillInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bp = other.gameObject.GetComponent<BasePlayer>();
        if (null == bp || bp.PlayerSide == ePlayerSide.ePlayer)
            return;


        EnemyList.Add(bp.transform);

    }

    private void OnTriggerExit(Collider other)
    {
        var bp = other.gameObject.GetComponent<BasePlayer>();
        if (null == bp)
            return;

        EnemyList.Remove(bp.transform);

    }
    #endregion

    #region Cast Attack

    void UpdateSkillInput()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            CastSkill(eSkillType.eAttack);
        }

    }
#endif
    void CastSkill(eSkillType type)
    {
        if (!IsReady || IsGetHit)
            return;

        SkillType = type;

        if (type == eSkillType.eSkill1)
        {
            CurAnimName = SkillPre + ((int)SkillType).ToString();
        }
        else if (type == eSkillType.eAttack)
        {
            if (_CurAnimAttackIndex > MaxAnimAttackIndex)
            {
                _CurAnimAttackIndex = MinAnimAttackIndex;
            }
            CurAnimName = AttackPre + _CurAnimAttackIndex.ToString();
        }

        AnimMgr.StartAnimation(CurAnimName, CastSkillReady, CastSkillBegin, CastSkillEnd, CastSkillEnd1);
    }

    void CastSkillReady()
    {
        IsReady = true;
    }

    void CastSkillBegin()
    {
        _IsPlaying = true;

        if (SkillType == eSkillType.eAttack)
        {
            IsReady = false;

            //���¼ĤH
            var Target = GlobalHelper.GetNearestTrans(EnemyList, transform);
            if (null != Target)
            {
                //var toward = (Target.position - transform.position).normalized;
                //toward.y = 0f;
                //transform.DOLookAt(Target.position, 0.1f);
            }
            //1 : ����Z���̪񪺼ĤH


            //���J�S��

            //�S�w��������S�w�S��
            //1001
            var path = SkillPrePath + (1000 + _CurAnimAttackIndex).ToString();
            var SkillPrefab = GlobalHelper.InstantiateMyPrefab(path, transform.position + Vector3.up * 1f, Quaternion.identity);

            SkillInfo = SkillPrefab.GetComponent<SEAction_SkillInfo>();
            SkillInfo.SetOwner(gameObject);

            _CurAnimAttackIndex++;
        }


    }
    #endregion

    void CastSkillEnd1()
    {

        //Vector2 Item = Vector2.zero;

        //if(SkillType == eSkillType.eAttack)
        //{
        //    if (_CurAnimAttackIndex <= 1)
        //    {
        //        Debug.LogError("Logic Error");
        //        return;
        //    }
        //    Item = AnimPerArray[_CurAnimAttackIndex - 2];


        //}
        //   else if(SkillType == eSkillType.eSkill1)
        //{
        //    Item = AnimSkillPerArray[(int)(SkillType - 1)];
        //}

        //WeaponInst.OnStartWeaponCtrl(Anim, Item.x, Item.y);
    }

    void CastSkillEnd()
    {
        if (SkillType == eSkillType.eAttack)
        {
            _CurAnimAttackIndex = MinAnimAttackIndex;
            IsReady = true;
        }

        var state = Anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Base Layer.GetHIt"))
        {

        }
        else
        {
            _IsPlaying = false;
        }

    }

    #region Final skill
    bool IsUsingAbility = false;
    bool IsFinishFinalSkill = false;
    Vector3 FinalSKillDir;

    public float FinalSkillDis = 1f;
    public void OnModifyFSV(int value)
    {
        //increase slider ->UI
        JoyStickInst.OnModifyFSV(value);
    }

    public void OnFinalSkillBegin(PointerEventData data)
    {

        if (IsUsingAbility == true)
            return;

        IsFinishFinalSkill = true;

        IsUsingAbility = true;
        Time.timeScale = 0.1f;

        _GroundArrow.SetActive(true);

        var dir = FinalSkillInst.Dir.x * Cam.transform.right + FinalSkillInst.Dir.y * Cam.transform.forward;

        dir.y = 0f;

        if (dir == Vector3.zero)
        {
            dir = transform.forward;
        }
        _GroundArrow.transform.forward = dir;
    }

    public void OnFinalSkillDrag(PointerEventData data)
    {
        if (!IsFinishFinalSkill)
            return;

        FinalSKillDir = FinalSkillInst.Dir.x * Cam.transform.right + FinalSkillInst.Dir.y * Cam.transform.forward;

        if (FinalSKillDir == Vector3.zero)
        {
            FinalSKillDir = transform.forward;
        }
        else
        {
            FinalSKillDir.y = 0f;
        }

        _GroundArrow.transform.forward = FinalSKillDir;
    }

    public void OnFinalSkillEnd(PointerEventData data)
    {
        if (!IsFinishFinalSkill)
            return;

        Time.timeScale = 1f;
        _GroundArrow.SetActive(false);
        FinalSKillDir = Vector3.zero;

        OnModifyFSV(-100);

        //����ޯ�ʵe
        CastSkill(eSkillType.eSkill1);

        var FinalPos = transform.position + _GroundArrow.transform.forward * FinalSkillDis;
        transform.DOMove(FinalPos, 0.7f).OnComplete(() => {
            IsUsingAbility = false;
            IsFinishFinalSkill = false;
        });
        transform.DOLookAt(FinalPos, 0.35f);
    }

    #endregion

    #region Load Arrow
    private GameObject _GroundArrow;
    public GameObject GroundArrow => (_GroundArrow);
    void LoadFinalSkillArrow()
    {
        var obj = Resources.Load("Weapons/GroundArrow");

        _GroundArrow = Instantiate(obj, transform.position, transform.rotation) as GameObject;

        _GroundArrow.transform.parent = transform;
        _GroundArrow.transform.localPosition = Vector3.zero;
        _GroundArrow.transform.localRotation = Quaternion.identity;
        _GroundArrow.transform.localScale = Vector3.one;

        _GroundArrow.SetActive(false);
    }
    #endregion

    #region Enemy Die
    public void EnemyDie(Transform enemy)
    {
        if (EnemyList.Contains(enemy))
        {
            EnemyList.Remove(enemy);
        }
    }


    #endregion

    #region Player GetHit
    bool IsGetHit = false;
    public void PlayerGetHIt()
    {

        if (Anim.IsInTransition(0))
            return;


        if(null!= SkillInfo)
        {
            SkillInfo.DestroyAllInst();
        }
        IsReady = true;
        _IsPlaying = true;
        IsGetHit = true;
        Anim.SetTrigger("Base Layer.GetHit");

    }

    #endregion

    #region animation callback
    void EventAnimEnd(int id)
    {
        eStateID ID = (eStateID)id;

        switch (ID)
        {
            case eStateID.eGetHit:
                {
                    _IsPlaying = false;
                    IsGetHit = false;
                    break;
                }
            case eStateID.eDie:
                {
                    //todo
                    //UIManager.Inst.OpenUI<UI_GameOver>();
                    break;
                }
        }
    }
    #endregion


    #region Create Palyer Actor
    public static AnimCtrl CreatePlayerActor(string RoleName,BirthPoint bp)
    {
        #region �Τ@
        //BGE_PlayerTemplate PlayerTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerTemplate>("PlayerTemplate", RoleName);

        //BGE_PlayerAttTemplate PlayerAttTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerAttTemplate>("PlayerAttTemplate", RoleName);
        ////�[���ҫ�
        //var tmp = Resources.Load(PlayerTpl.f_ModelPath);

        //var actor = Instantiate(tmp,bp.transform.position,bp.transform.rotation)as GameObject;

        //actor.name = tmp.name;
        ////�[���}��
        //var ret = actor.AddComponent<AnimCtrl>();

        ////��l�ƩҦ��ƾ�


        //ret.PlayerName = RoleName;

        //ret.TypeId = PlayerTpl.f_TypeID;

        //ret.PlayerTpl = PlayerTpl;

        //ret.PlayerAttTpl = PlayerAttTpl;

        //ret.PlayerSide = (ePlayerSide)PlayerTpl.f_PlayerSide;

        //ret.AnimPerArray = PlayerAttTpl.f_AnimPerArray.ToArray();

        //ret.AnimSkillPerArray = PlayerAttTpl.f_AnimPerSkillArray.ToArray();

        //ret.transform.localScale = Vector3.one * bp.Scale;
        #endregion
        var ret = CreateBaseActor<AnimCtrl>(RoleName, bp);

        ret.FinalSkillDis = ret.PlayerAttTpl.f_FinalSkillDis;
        //�[��JoyStick
        ret.JoyStickInst = UIManager.Inst.OpenUI<UI_JoyStick>();

        ret.JoyStickInst.OnStart();

        //�K�[movement input

        ret.MoveInput = ret.gameObject.AddComponent<Movementinput>();
        ret.MoveInput.OnStart(ret);
        //input.OnStart(ret);

        //��^AnimCtrl
        return ret;
    }
    #endregion

    #region Player Death

    public void SetPlayerGameOver(bool iswin)
    {
        //close UIJoyStick
        JoyStickInst.gameObject.SetActive(false);
        //Play ���`�ʵe
        if (iswin)
        {
            Anim.SetTrigger("Base Layer.Victory");
        }
        else
        {
            Anim.SetTrigger("Base Layer.Die");
        }

        UIManager.Inst.OpenUI<UI_GameOver>();
        //close collider
        CharacCtrl.enabled = false;
        MoveInput.IsActive = false;

    }
    #endregion
}
