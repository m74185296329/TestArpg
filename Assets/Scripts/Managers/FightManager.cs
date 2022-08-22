using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    CamManager CamMgr;
    public void Awake()
    {
        var GOCam = Instantiate(Resources.Load("Maps/Cams")) as GameObject;
        CamMgr = GOCam.GetComponent<CamManager>();
    }
    //�ҰʹC��:�۰ʼu�XUI_Play �I��UI_Play�����s->Ū������M������UI

    //�[��Play UI
    public BirthPoint BP;

    public BirthPoint EnemyBP;
    UI_Login UILoginInst;

    private void Start()
    {
        //var login = Instantiate(Resources.Load("UI/UI_Login"))as GameObject;

        //UILoginInst = login.GetComponent<UI_Login>();

        UILoginInst = UIManager.Inst.OpenUI<UI_Login>();
        UILoginInst.OnStart(BP,EnemyBP, CamMgr);
    }

    
}
