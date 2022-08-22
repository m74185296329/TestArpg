using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementinput : MonoBehaviour
{
    #region System Function
    // Start is called before the first frame update
    public void OnStart(AnimCtrl ac)
    {
        Cam = Camera.main;
        AnimCtrlInst = ac;

        CharCtrl = ac.CharacCtrl;

        JoyStick = ac.JoyStickInst;

        Anim = ac.Anim;
    }

    // Update is called once per frame
    void Update()
    {

        if (null == AnimCtrlInst)
            return;

        if (CanMove())
        {
            SetPlayerAnimMoveParn();
        }
    }
    #endregion
    #region Player Animation Controller
    private AnimCtrl AnimCtrlInst;
    Animator Anim;
    CharacterController CharCtrl;
    UI_JoyStick JoyStick;
    float horizontal;
    float vertical;
    float speed;
    float s1;
    float s2;

    [HideInInspector]
    public bool IsActive = true;

    Camera Cam;

    bool CanMove()
    {
        if (!IsActive)
            return false;

        if (AnimCtrlInst.IsPlaying)//¬O§_§ðÀ»ª¬ºA
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    void SetPlayerAnimMoveParn()
    {
#if UNITY_EDITOR
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        s1 = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
        s2 = null != JoyStick ? JoyStick.Dir.magnitude : 0;

        speed = s1 > s2 ? s1 : s2;

        if(s2 > s1)
        {
            horizontal = JoyStick.Dir.x;
            vertical = JoyStick.Dir.y;
        }
     
#else
        speed = JoyStick.Dir.magnitude;
#endif
        speed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);

        Anim.SetFloat("IdleAndRun", speed);

        if(speed > 0.01f)
        {
            PlayerCtrlMovement(horizontal, vertical);
        }
    }
    void PlayerCtrlMovement(float x,float z) 
    {
        var dir = x * Cam.transform.right + z * Cam.transform.forward;

        dir.y = 0f;

        transform.forward = dir;

        CharCtrl.Move(AnimCtrlInst.BaseAttr.Speed * Time.deltaTime * dir);
    }
#endregion
}
