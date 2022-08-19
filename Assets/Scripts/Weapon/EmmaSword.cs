using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmaSword : MonoBehaviour
{

    //根據攻擊動畫播放比率控制hitbox
    #region Init
    BoxCollider BC;
    Animator Anim;
    float StartPer;
    float EndPer;
    float curPer;
    float lastPer;
    AnimatorStateInfo StateInfo;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider>();
        BC.enabled = false;
    }

    AnimCtrl AnimCtrlInst;
    public void OnStart(AnimCtrl ac)
    {
        AnimCtrlInst = ac;
    }


    public void OnStartWeaponCtrl(Animator _Anim,float _StartPer,float _EndPer)
    {
        StartPer = _StartPer;
        EndPer = _EndPer;
        Anim = _Anim;
        StopAllCoroutines();
        //檢測動畫進度
        StartCoroutine(WaitToPlayAnim());
    }

    IEnumerator WaitToPlayAnim()
    {
        //var stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        //stateInfo.normalizedTime %1.0f < 1.0f
        while (true)
        {
            StateInfo = Anim.GetCurrentAnimatorStateInfo(0);
            curPer = StateInfo.normalizedTime % 1.0f;
            if(curPer >= StartPer && lastPer < StartPer)
            {
                BC.enabled = true;
            }
            else if(curPer > EndPer && lastPer <= EndPer)
            {
                BC.enabled = false;
                break;
            }

            lastPer = curPer;
            yield return null;
        }

       
    }

    //在有效期間內,如果碰到敵人,直接把hitbox關閉
    private void OnTriggerEnter(Collider other)
    {
        var enemyActor = other.gameObject.GetComponent<NpcActor>();
        if(enemyActor != null)
        {
            enemyActor.GetHit();

            //increase player slider
            AnimCtrlInst.OnModifyFSV(50);
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
