using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_JoyStick : MonoBehaviour
{

    #region Sys
    private void Start()
    {
        FinalSkillBtnInst.SetFinalSkillState(ShowFinalSkillBtn);
    }
    #endregion
    #region JoyStick
    public CommonJoyBtn CommonBtn;

    public Vector3 Dir => (CommonBtn.Dir);
    #endregion
    #region slider
    public Slider SliderInst;
    public int AngryIncrease = 10;
    public Image HightLight1;
    public Image HightLight2;
    public bool ShowFinalSkillBtn => (SliderInst.value >= 100);

    public void OnModifyFSV()
    {
        var angryValue = SliderInst.value;
        SliderInst.value += AngryIncrease;

        if(SliderInst.value >= 100 && angryValue < 100)
        {
            HightLight1.enabled = true;
        }
        else if(SliderInst.value >= 200 && angryValue < 200)
        {
            HightLight2.enabled = true;
        }

        FinalSkillBtnInst.SetFinalSkillState(ShowFinalSkillBtn);
    }
    #endregion

    #region FinalSkill
    public FinalSkillBtn FinalSkillBtnInst;

    #endregion

}
