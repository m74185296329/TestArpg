
using UnityEngine;
using AttTypeDefine;
using com.dxz.config;

public class NpcActor : BasePlayer
{
    #region Paras
    UI_HUD NpcHUD;
    [HideInInspector]
    public BasePlayer PlayerInst;

    NpcAICtrl AICtrl;
    #endregion



    #region Sys
    protected override void Awake()
    {
        base.Awake();
       
    }

    protected override void Start()
    {
        base.Start();
        AICtrl.OnStart(this);
    }

    public void OnStart(AnimCtrl player)
    {
        PlayerInst = player;
        AICtrl = gameObject.AddComponent<NpcAICtrl>();
        AICtrl.OnStart(this);
    }

    public void Update()
    {
        NpcHUD?.SetHUDPos(this);
    }
    #endregion

    #region HUD & GetHit
    public void GetHit()
    {
        Anim.SetTrigger("Base Layer.GetHit");
    }


    
    public void UpdateHp (float hp)
    {
        NpcHUD?.UpdateHp(hp);
    }
    #endregion


    #region Npc AI Ctrl

    public void SetAIState(eStateID state)
    {
        AICtrl.NpcState = state;
    }

    #endregion

    #region Load Enemy
    public static NpcActor CreateNpcActor(string RoleName, BirthPoint bp)
    {
        var ret = CreateBaseActor<NpcActor>(RoleName, bp);
        //load hud
        ret.NpcHUD = UIManager.Inst.OpenUI<UI_HUD>();
        return ret;
    }
    #endregion
}
