using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;

public class FinalSkillBtn : CommonJoyBtn
{
    public Color NormalColor;
    public Color DisabledColor;
    public CanvasGroup CanvasGpInst;

    public override void Awake()
    {
        
    }

    public void Init()
    {
        PressDown = new GameBtnEvent();
        OnDragEvent = new GameBtnEvent();
        PressUp = new GameBtnEvent();
    }

    public void SetFinalSkillState (bool on)
    {

        CanvasGpInst.blocksRaycasts = on;

        ImageBackground.color = (on == true) ? NormalColor : DisabledColor;
        ImageHandle.color = (on == true) ? NormalColor : DisabledColor;
        /*if(on)
        {
            ImageBackground.color = NormalColor;
            ImageHandle.color = NormalColor;
        }
        else
        {
            ImageBackground.color = DisabledColor;
            ImageHandle.color = DisabledColor;
        }*/
    }
}
