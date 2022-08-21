using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AttTypeDefine
{
    public delegate void NotifySkill();


    public enum eSkillBindType
    {
        eEffectWorld,
        eEffectOwner,
        eDamageOwner,
    }



    public enum ePlayerAttr
    {
        eNULL = -1,
        eHP = 0,
        eMaxHP = 1,
        eAttack = 2,
        eSize,
    }

    public enum eStateID
    {
        eNULL = -1,
        eIdle = 0,
        eChase = 1,
        eAttack = 2,
        eGetHit = 3,
        eFlyAway = 4,//����
        eDie = 5,
    }

    public enum ePlayerSide
    {
        ePlayer,
        eEnemy,
        eNPC,
    }
    public enum eTrigType
    {
        eAuto =0,
        eCondition,
    }


    public enum eSkillType
    {
        eAttack = 0,
        eSkill1,
    }

    public enum eTrigSkillState
    {
        eTrigBegin,
        eTrigEnd,
    }

    public class GameEvent : UnityEvent
    {

    }

    public class GameBtnEvent : UnityEvent<PointerEventData>
    {

    }

}