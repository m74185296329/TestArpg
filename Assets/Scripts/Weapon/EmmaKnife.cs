using System.Collections;
using UnityEngine;

public class EmmaKnife : MonoBehaviour
{
    #region Paras
    BoxCollider BC;
    Animator Anim;
    float StartPer;
    float EndPer;
    float curPer;
    float lastPer;
    AnimatorStateInfo StateInfo;
    #endregion

    #region sys
    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider>();
        BC.enabled = false;
    }
    #endregion

    #region weapon mgr

    AnimCtrl AnimCtrlInst;
    public void OnStart(AnimCtrl ac)
    {
        AnimCtrlInst = ac;
    }

    public void OnStartWeaponCtrl(Animator _Anim, float _StartPer, float _EndPer)
    {
        StartPer = _StartPer;
        EndPer = _EndPer;
        Anim = _Anim;
        StopAllCoroutines();
        //��⵱ǰ�����İٷֱ�
        StartCoroutine(WatiToPlayAnim());
    }


    IEnumerator WatiToPlayAnim()
    {
        while (true)
        {
            StateInfo = Anim.GetCurrentAnimatorStateInfo(0);
            curPer = StateInfo.normalizedTime % 1.0f;
            if (curPer >= StartPer && lastPer < StartPer)
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


    //����Ч�������ڣ�����������ˣ���ôֱ�ӰѴ󵶵Ļ��Թر�.

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        var enemyActor = other.gameObject.GetComponent<NpcActor>();
        if(enemyActor != null)
        {
            enemyActor.GetHit();

            //player increase angry value;
            AnimCtrlInst.OnModifyFSV(25);
        }


    }


    #endregion
}
