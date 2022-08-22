using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Login : UIBase
{

    public void OnLogin()
    {
        FightManager.Inst.GameProcedure = AttTypeDefine.eGameProcedure.eFightStart;

        UIManager.Inst.CloseUI<UI_Login>(this,true);
    }

}
