using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
public class FightManager : MonoBehaviour
{

    private static FightManager inst;

    public static FightManager Inst => (inst);

    AnimCtrl PlayerInst;

    List<BasePlayer> EnemyList;

    public int LeftEnemyCount => (EnemyList.Count);

    CamManager CamMgr;
    public BirthPoint BP;

    public BirthPoint[] EnemyBP;



    eGameProcedure gameprocedure = eGameProcedure.eNULL;
    public eGameProcedure GameProcedure
    {
        get
        {
            return gameprocedure;
        }
        set
        {
            if(value != gameprocedure)
            {
                switch (value)
                {
                    case eGameProcedure.eFightStart:
                        {
                            //加載玩家

                            PlayerInst = AnimCtrl.CreatePlayerActor(ConstData.PlayerName, BP);
                            //啟動相機
                            CamMgr.OnStart(PlayerInst);
                            //加載敵人
                            for(var i = 0; i < EnemyBP.Length; i++)
                            {
                                var enemy = NpcActor.CreateNpcActor(ConstData.SkeleName, EnemyBP[i]);
                                enemy.OnStart(PlayerInst);
                                AddEnemy(enemy);
                            }
                            
                            break;
                        }
                    /*case eGameProcedure.eFighting:
                        {
                            break;
                        }*/
                    case eGameProcedure.eFightOver:
                        {

                            if (PlayerInst.BaseAttr[ePlayerAttr.eHP] == 0 && EnemyList.Count >0)
                            {
                                PlayerInst.SetPlayerGameOver(false);
                                SetEnemyVictory();
                            }
                            else if (EnemyList.Count == 0 && PlayerInst.BaseAttr[ePlayerAttr.eHP]>0)
                            {
                                PlayerInst.SetPlayerGameOver(true);
                            }
                            break;
                        }
                    case eGameProcedure.eRestart:
                        {
                            //清除現有物件
                            //載入所需數據
                            RestartGame();
                            break;
                        }
                }
                gameprocedure = value;
            }

        }
    }


    #region Enemy Mgr
    void AddEnemy(BasePlayer bp) 
    { 
        EnemyList.Add(bp);
    }

    public void RemoveEnemy(BasePlayer bp) 
    { 
        EnemyList.Remove(bp);

    }
    #endregion

    public void SetEnemyVictory()
    {
        for(var i = 0; i < EnemyList.Count; i++)
        {
            var item = EnemyList[i];
            ((NpcActor)item).SetAIState(eStateID.eVictory);
        }
    }

    public void Awake()
    {
        inst = this;
        EnemyList = new List<BasePlayer>();
        var GOCam = Instantiate(Resources.Load("Maps/Cams")) as GameObject;
        CamMgr = GOCam.GetComponent<CamManager>();
        
    }
    //啟動遊戲:自動彈出UI_Play 點擊UI_Play的按鈕->讀取角色和對應的UI

    //加載Play UI
    

    private void Start()
    {
        //var login = Instantiate(Resources.Load("UI/UI_Login"))as GameObject;

        //UILoginInst = login.GetComponent<UI_Login>();

        UIManager.Inst.OpenUI<UI_Login>();
    }

    private void Update()
    {
        
    }

    void RestartGame()
    {
        Destroy(PlayerInst.gameObject);
        while (EnemyList.Count > 0)
        {
            var item = EnemyList[0];
            EnemyList.Remove(item);
            NpcActor.DestroySelf((NpcActor)item);
        }

        
        UIManager.Inst.OpenUI<UI_Login>();
    }

}
