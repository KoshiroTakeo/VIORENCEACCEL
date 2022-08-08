//============================================================
// Controller.cs
//======================================================================
// �J������
//
// 
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*************************************
// �v���C���[�̑���Ǘ�
//*************************************

public class PlayerManager : PlayerStatus
{
    //OVRManager�g�p
    OVRInput.Controller rightController, leftController;
    OVRPlayerController PlayerController;
    ActionManager ActionManager = null;
    MoveManager MoveManager = null;
    //public SoundManager SoundManager;
    [SerializeField] GameObject AnchorObj = null;

    Vector3 InitirizeAnchorPos = new Vector3();

    
    float fSetAcceleration = 0;
    float fSetRotationAmount = 0;
    bool bBoosterOn = false;

    private void Start()
    {
        InitPlayerManager();
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

    // �����蔻��iIsTrriger�j===========================================================
    private void OnControllerColliderHit(ControllerColliderHit collider)
    {

        // �_���[�W���� ===================================
        if (collider.gameObject.tag == "EnemyAttack")
        {
            IsDamage();
        }
        //=================================================
    }
    //===================================================================================

    // �E��̑��� =======================================================================
    void RightControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // ����̎g�p
            ActionManager.Attack_R();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            // ����̌���
            ActionManager.RightChangeWepon();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            InitirizeAnchorPos = AnchorObj.transform.position;
            bBoosterOn = !bBoosterOn;

        }



    }
    //===================================================================================

    // ����̑��� =======================================================================
    void LeftControllerDevice()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            // ����̎g�p
            ActionManager.Attack_L();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            // ����̌���
            ActionManager.LeftChangeWepon();
        }

        
    }
    //===================================================================================

    // �_���[�W���� =====================================================================
    void IsDamage()
    {
        //����炷
        //SoundManager.Play_PlayerDamage(PlayerObject);

        //�_���[�W��1�`100�̒��Ń����_���Ɍ��߂�B
        int damage = Random.Range(1, 3);


        //���݂�HP����_���[�W������
        nLife = nLife - damage;

        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B
        //int���m�̊���Z�͏����_�ȉ���0�ɂȂ�̂ŁA
        //(float)������float�̕ϐ��Ƃ��ĐU���킹��B

        if(nLife <= 0)
        {
            bDead = true;
        }
    }
    //===================================================================================

   // ���������� ========================================================================
   void InitPlayerManager()
    {
        
        ActionManager = this.gameObject.GetComponent<ActionManager>();
        if(ActionManager == null) Debug.Log("�uActionManager.cs�v�����̃I�u�W�F�N�g�ɐݒ肵�Ă�������");
        
        MoveManager = this.gameObject.GetComponent<MoveManager>();
        if (MoveManager == null) Debug.Log("�uMoveManager.cs�v�����̃I�u�W�F�N�g�ɐݒ肵�Ă�������");

        PlayerController = this.gameObject.GetComponent<OVRPlayerController>();
        if (PlayerController == null) Debug.Log("�uPlayerController.cs�v�����̃I�u�W�F�N�g�ɐݒ肵�Ă�������");
        
        rightController = OVRInput.Controller.RTouch;
        leftController = OVRInput.Controller.LTouch;

        fSetAcceleration = PlayerController.Acceleration;
        fSetRotationAmount = PlayerController.RotationAmount;

        AnchorObj = new GameObject("AnchorObject");
        MoveAnchor moveAnchor = AnchorObj.AddComponent<MoveAnchor>();
        moveAnchor.Centereye = this.gameObject.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").gameObject;
        moveAnchor.PlayerObj = this.gameObject;

        
    }
    //===================================================================================
}
