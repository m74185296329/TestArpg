using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{

    public CinemachineFreeLook FreeLook;

    AnimCtrl Owner;
    Camera Cam;
    private void Awake()
    {
        Cam = Camera.main;
    }

    // Start is called before the first frame update
    public void OnStart(AnimCtrl player)
    {
        Owner = player;
        FreeLook.LookAt = Owner.transform;
        FreeLook.Follow = Owner.transform;
    }
}
