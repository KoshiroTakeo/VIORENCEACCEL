//============================================================
// Controller.cs
//======================================================================
// 開発履歴
//
// 
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*************************************
// プレイヤーの操作管理
//*************************************

public class PlayerManager : PlayerStatus
{
    //OVRManager使用
    OVRInput.Controller rightController, leftController;
    OVRPlayerController PlayerController;
    ActionManager ActionManager = null;
    MoveManager MoveManager = null;
    [SerializeField] SoundManager SoundManager = null;
    [SerializeField] GameObject AnchorObj = null;

    Vector3 InitirizeAnchorPos = new Vector3();

    
    float fSetAcceleration = 0;
    float fSetRotationAmount = 0;
    bool bBoosterOn = false;

    private void Start()
    {

        rightController = OVRInput.Controller.RTouch;
        leftController = OVRInput.Controller.LTouch;

        ActionManager = this.gameObject.GetComponent<ActionManager>();
        MoveManager = this.gameObject.GetComponent<MoveManager>();
        PlayerController = this.gameObject.GetComponent<OVRPlayerController>();
        fSetAcceleration = PlayerController.Acceleration;
        fSetRotationAmount = PlayerController.RotationAmount;

        AnchorObj = GameObject.FindWithTag("MoveAnchor");
        
    }

    private void Update()
    {
        RightControllerDevice();

        LeftControllerDevice();

        MoveManager.CapsuleFollowHeadset();


        if(bBoosterOn == true)
        {
            PlayerController.Acceleration = 0;
            PlayerController.RotationAmount = 0;

            MoveManager.HeadInclinationMove(AnchorObj, fSpeed, InitirizeAnchorPos);
        }
        else if(bBoosterOn == false)
        {
            PlayerController.Acceleration = fSetAcceleration;
            PlayerController.RotationAmount = fSetRotationAmount;
        }
        

        MoveManager.Gravity();
    }

    // あたり判定（IsTrriger）===========================================================
    private void OnControllerColliderHit(ControllerColliderHit collider)
    {

        // ダメージ判定 ===================================
        if (collider.gameObject.tag == "EnemyAttack")
        {
            IsDamage();
        }
        //=================================================
    }
    //===================================================================================

    // 右手の操作 =======================================================================
    void RightControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // 武器の使用
            ActionManager.Attack_R();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            // 武器の交換
            ActionManager.RightChangeWepon();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            InitirizeAnchorPos = AnchorObj.transform.position;
            bBoosterOn = !bBoosterOn;

        }



    }
    //===================================================================================

    // 左手の操作 =======================================================================
    void LeftControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            // 武器の使用
            ActionManager.Attack_L();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            // 武器の交換
            ActionManager.LeftChangeWepon();
        }

        
    }
    //===================================================================================

    // ダメージ処理 =====================================================================
    void IsDamage()
    {
        //音を鳴らす
        SoundManager.Play_PlayerDamage(PlayerObject);

        //ダメージは1～100の中でランダムに決める。
        int damage = Random.Range(1, 3);


        //現在のHPからダメージを引く
        nLife = nLife - damage;

        //最大HPにおける現在のHPをSliderに反映。
        //int同士の割り算は小数点以下は0になるので、
        //(float)をつけてfloatの変数として振舞わせる。

        if(nLife <= 0)
        {
            bDead = true;
        }
    }
    //===================================================================================

   
}
