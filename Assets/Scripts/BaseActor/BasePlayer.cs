using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
using com.dxz.config;

public class BasePlayer : MonoBehaviour
{

    protected BGE_PlayerTemplate PlayerTpl;
    protected BGE_PlayerAttTemplate PlayerAttTpl;

    private float playerraidus;
    public float PlayerRadius => (playerraidus);

    protected AnimatorManager AnimMgr;



    private CharacterController characterCtrl;

    public CharacterController CharacCtrl => (characterCtrl);

    public float PlayerHeight => (characterCtrl.height);

    [HideInInspector]
    public string PlayerName;
    //hp,attack

    private BaseAttributes _BaseAttr;
    public BaseAttributes BaseAttr => (_BaseAttr);

    [HideInInspector]
    public Vector3 ClosestHitPoint;

    public Vector2[] AnimPerArray;
    public Vector2[] AnimSkillPerArray;

    public ePlayerSide PlayerSide;

    protected Animator _Anim;
    public Animator Anim => (_Anim);

    protected int TypeId;

    public int TypeID => (TypeId);

    protected string roleId;
    public string RoleID => (roleId);
    protected virtual void Awake()
    {
        _BaseAttr = gameObject.AddComponent<BaseAttributes>();

        characterCtrl = GetComponent<CharacterController>();

        AnimMgr = gameObject.AddComponent<AnimatorManager>();

        _Anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        _BaseAttr.InitPlayerAttr(this,PlayerName);

        playerraidus = characterCtrl.radius * transform.localScale.x;
    }

    /*public void PlayAnim(string animName)
    {
        _Anim.SetTrigger(animName);
    }*/

    protected static  T CreateBaseActor<T> (string RoleName,BirthPoint bp) where T : BasePlayer
    {
        BGE_PlayerTemplate PlayerTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerTemplate>("PlayerTemplate", RoleName);

        BGE_PlayerAttTemplate PlayerAttTpl = GlobalHelper.GetTheEntityByName<BGE_PlayerAttTemplate>("PlayerAttTemplate", RoleName);
        //更家
        var tmp = Resources.Load(PlayerTpl.f_ModelPath);

        var actor = Instantiate(tmp, bp.transform.position, bp.transform.rotation) as GameObject;

        actor.name = tmp.name;
        //更竲セ
        var ret = actor.AddComponent<T>();

        //﹍て┮Τ计沮


        ret.PlayerName = RoleName;

        ret.TypeId = PlayerTpl.f_TypeID;

        if (null != PlayerAttTpl.f_AnimPerArray)
            ret.AnimPerArray = PlayerAttTpl.f_AnimPerArray.ToArray();

        if (null != PlayerAttTpl.f_AnimPerSkillArray)
            ret.AnimSkillPerArray = PlayerAttTpl.f_AnimPerSkillArray.ToArray();

        ret.PlayerTpl = PlayerTpl;

        ret.PlayerAttTpl = PlayerAttTpl;

        ret.PlayerSide = (ePlayerSide)PlayerTpl.f_PlayerSide;

        //load hud

        //load Animator
        ret.Anim.runtimeAnimatorController = Instantiate(Resources.Load("AnimatorController/" + PlayerTpl.f_AnimCtrlPath)) as RuntimeAnimatorController;

        ret.transform.localScale = Vector3.one * bp.Scale;
        //NpcCtrl
        return ret;
    }
}
