using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*************************************
// プレイヤーの操作管理
//*************************************

public class Controller : MonoBehaviour
{
    //OVRManager使用
    OVRInput.Controller rightController, leftController;
    ActionManager actionManager;

    private void Start()
    {
        rightController = OVRInput.Controller.RTouch;
        leftController = OVRInput.Controller.LTouch;

        actionManager = this.GetComponent<ActionManager>();
    }

    private void Update()
    {
        RightControllerDevice();

        LeftControllerDevice();

        
    }

    void RightControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // 武器の使用
            actionManager.Attack_R();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            // 武器の交換
            actionManager.RightChangeWepon();
        }
    }

    void LeftControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            // 武器の使用
            actionManager.Attack_L();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            // 武器の交換
            actionManager.LeftChangeWepon();
        }
    }
}
