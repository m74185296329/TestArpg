using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tur1 : MonoBehaviour
{
    public Animator Anim;
    public void Atk()
    {
        Anim.SetTrigger("Base Layer.Atk");
    }
    public void Jump()
    {
        Anim.SetTrigger("Base Layer.Jump");
    }
    public void Idle()
    {
        Anim.SetTrigger("Base Layer.Idle");
    }
}
