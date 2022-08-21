
using UnityEngine;
using AttTypeDefine;
using System.Collections.Generic;

public class SEActionDamage_BindOwner : SEAction_BaseAction
{
    public SEAction_TrigBuff TrigBuffInst;
    [HideInInspector]
    public string SocketName;
    [HideInInspector]
    public Vector3 OffSet;
    [HideInInspector]
    public Vector3 OffRot;

    GameObject Owner;

    bool IsTriggered = false;

    BoxCollider BC;

    Animator Anim;

    float StartCollidePercent;
    float EndCollidePercent;

    BasePlayer bp;

    List<BasePlayer> BaList;

    private void Awake()
    {
        BaList = new List<BasePlayer>();
    }


    public override void TrigAction()
    {
        base.TrigAction();

        var ds = GetDataStore();

        if (ds == null)
        {
            return;
        }

        Owner = ds.Owner;

        //獲取接點
        var socket = GlobalHelper.FindGOByName(Owner, SocketName);

        if (socket == null)
        {
            socket = Owner;
        }

        transform.parent = socket.transform;
        transform.localPosition = OffSet;
        transform.localRotation = Quaternion.Euler(OffRot);


        BC = GetComponent<BoxCollider>();

        

        Anim = Owner.GetComponent<Animator>();

        if(null == Anim)
        {
            return;
        }


        bp = Owner.GetComponent<BasePlayer>();

        var skillName = int.Parse(ds.SkillInfo.name);

        var index = skillName - bp.TypeID;

        if(index < 10)
        {
            StartCollidePercent = bp.AnimPerArray[index - 1].x;
            EndCollidePercent = bp.AnimPerArray[index - 1].y;
        }
        else
        {
            StartCollidePercent = bp.AnimSkillPerArray[index - 1].x;
            EndCollidePercent = bp.AnimSkillPerArray[index - 1].x;
        }


        IsTriggered = true;
    }

    AnimatorStateInfo ASI;

    float curPer;
    float lastPer;
    protected override void Update()
    {

        base.Update();
        if (!IsTriggered)
        {
            return;
        }


        if(BaList.Count > 0)
        {
            var ba = BaList[0];
            BaList.Remove(ba);
            var ds = GetDataStore();
            ds.Target = ba.gameObject;
            //trig增益 -> 實例化增益
            TrigBuffInst.OnStart();
        }

        if (Anim.IsInTransition(0))
        {
            return;
        }

        //判斷是否開啟hitbox->動畫進度
        ASI = Anim.GetCurrentAnimatorStateInfo(0);

        curPer = ASI.normalizedTime % 1.0f;
        if (curPer >= StartCollidePercent && lastPer < StartCollidePercent)
        {
            BC.enabled = true;
        }
        else if (curPer > EndCollidePercent && lastPer <= EndCollidePercent)
        {
            BC.enabled = false;
            Destroy(gameObject);
        }

        lastPer = curPer;


    }

    private void OnTriggerEnter(Collider other)
    {

        
        BasePlayer ba = other.gameObject.GetComponent<BasePlayer>();
        if(null == ba)
        {
            return;
        }
        else
        {
            //陣營判斷

            var Attacker = bp;
            var Defenser = ba;

            if(
                (bp.PlayerSide == ePlayerSide.eEnemy && ba.PlayerSide == ePlayerSide.ePlayer)||
                (bp.PlayerSide == ePlayerSide.ePlayer && ba.PlayerSide == ePlayerSide.eEnemy)
              )
            {
                //var ClosestPoint = other.ClosestPoint(other.gameObject.transform.position);



                var dir = (Attacker.transform.position - Defenser.transform.position).normalized;

                var ClosestPoint = other.bounds.center + dir * other.bounds.extents.z;//other.ClosestPointOnBounds(other.gameObject.transform.position);
                Defenser.ClosestHitPoint = ClosestPoint;
                BaList.Add(ba);
            }

        }
    }
}
