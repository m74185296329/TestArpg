
using UnityEngine;
using UnityEngine.UI;
public class UI_HUD : UIBase
{

    public Slider HPSlider;

    Camera Cam;

    public float Duration;

    private float StartTime;

    private bool IsTrigger = false;

    private void Start()
    {
        Cam = Camera.main;
    }

    //開始,進行中
    //在進行中，再次進行就要重算
    public void SetHUDPos(BasePlayer NpcTrans)
    {
        if (IsTrigger) 
        {
            HPSlider.transform.position = Cam.WorldToScreenPoint(NpcTrans.transform.position + Vector3.up * NpcTrans.PlayerHeight * 0.7f);
        }
        
        
    }

    private void Update()
    {
        if (IsTrigger)
        {
            if(Time.time - StartTime > Duration)
            {
                gameObject.SetActive(false);
                IsTrigger = false;
            }
        }
    }

    public void UpdateHp(float hp)
    {
        //NpcHUD.UpdateHp(hp);
        HPSlider.value = hp;
        StartTime = Time.time;
        IsTrigger = true;
        gameObject.SetActive(true);
    }
}
