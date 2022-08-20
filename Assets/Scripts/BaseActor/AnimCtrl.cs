using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AttTypeDefine;
using DG.Tweening;

public class AnimCtrl : MonoBehaviour
{
    #region Sys funcs
    public Vector2[] AnimPerArray;
    public Vector2[] AnimSkillPerArray;
    public UI_JoyStick JoyStickInst;

    FinalSkillBtn FinalSkillInst;
    AnimatorManager AnimMgr;
    Animator _Anim;
    int _CurAnimAttackIndex = 1;
    int MinAnimAttackIndex = 1;
    int MaxAnimAttackIndex = 3;
    string CurAnimName;
    string AttackPre = "Base Layer.Atk";
    string SkillPre = "Base Layer.Skill";
    string SkillPrePath = "Skills/";
    bool IsReady = true;

    Camera Cam;

    EmmaSword WeaponInst;


    bool _IsPlaying;
    public bool IsPlaying =>(_IsPlaying);

    eSkillType SkillType;
    public Animator Anim => (_Anim);
    private void Awake()
    {
        AnimMgr = gameObject.AddComponent<AnimatorManager>();

        

    }
    private void Start()
    {
        _Anim = GetComponent<Animator>();
        AnimMgr.OnStart(this);

        FinalSkillInst = JoyStickInst.FinalSkillBtnInst;

        Cam = Camera.main;

        var weapongo = GlobalHelper.FindGOByName(gameObject, "greatesword");
        if (null != weapongo)
        {
            WeaponInst = weapongo.GetComponent<EmmaSword>();
            WeaponInst.OnStart(this);
        }

        JoyStickInst.FinalSkillBtnInst.PressDown.AddListener((a) => OnFinalSkillBegin(a));
        JoyStickInst.FinalSkillBtnInst.OnDragEvent.AddListener((a) => OnFinalSkillDrag(a));
        JoyStickInst.FinalSkillBtnInst.PressUp.AddListener((a) => OnFinalSkillEnd(a));

        LoadFinalSkillArrow();

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
            CastSkill(eSkillType.eAttack);
        }
      
    }
#endif
    void CastSkill(eSkillType type)
    {
        if (!IsReady)
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
        
        AnimMgr.StartAnimation(CurAnimName,CastSkillReady, CastSkillBegin,CastSkillEnd,CastSkillEnd1);
    }

    void CastSkillReady()
    {
        IsReady = true;
    }

    void CastSkillBegin()
    {
        _IsPlaying = true;

        if(SkillType == eSkillType.eAttack)
        {
            IsReady = false;

            

            //載入特效

            //特定攻擊播放特定特效
            //1001
            var path = SkillPrePath + (1000 + _CurAnimAttackIndex).ToString();
            var SkillPrefab = GlobalHelper.InstantiateMyPrefab(path,transform.position + Vector3.up*1f,Quaternion.identity);

            var SkillInfo = SkillPrefab.GetComponent<SEAction_SkillInfo>();
            SkillInfo.SetOwner(gameObject);
            _CurAnimAttackIndex++;
        }

        
    }
    #endregion

    void CastSkillEnd1()
    {

        Vector2 Item = Vector2.zero;

        if(SkillType == eSkillType.eAttack)
        {
            if (_CurAnimAttackIndex <= 1)
            {
                Debug.LogError("Logic Error");
                return;
            }
            Item = AnimPerArray[_CurAnimAttackIndex - 2];

            
        }
           else if(SkillType == eSkillType.eSkill1)
        {
            Item = AnimSkillPerArray[(int)(SkillType - 1)];
        }

        WeaponInst.OnStartWeaponCtrl(Anim, Item.x, Item.y);
    }

    void CastSkillEnd()
    {
        if(SkillType == eSkillType.eAttack)
        {
            _CurAnimAttackIndex = MinAnimAttackIndex;
            IsReady = true;
        }
        _IsPlaying = false;
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

        if(dir == Vector3.zero)
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

        //播放技能動畫
        CastSkill(eSkillType.eSkill1);

        var FinalPos = transform.position + _GroundArrow.transform.forward * FinalSkillDis;
        transform.DOMove(FinalPos, 0.7f).OnComplete(()=> {
            IsUsingAbility = false;
            IsFinishFinalSkill = false;
        });
        transform.DOLookAt(FinalPos,0.35f);
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

}
