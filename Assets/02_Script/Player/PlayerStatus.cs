//============================================================
// PlayerStatus.cs
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



public class PlayerStatus : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    protected GameObject PlayerObject; 
    protected GameObject HeadAnchor;                                                   // ���̃g���b�L���O
    protected GameObject PlayerLeftArmAnchor, PlayerRightArmAnchor;                    // ����̃g���b�L���O
    protected GameObject L_FirstWeapon, L_SecondWeapon, R_FirstWeapon, R_SecondWeapon; // �������ꂽ����
    

    [Header("�X�e�[�^�X")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;
    //public float fHeight = 1.8f; //�v���C���[�̐g����ݒ�

    [Header("���")]
    protected bool bDead = false;

    

    private void Awake()
    {
        PlayerObject = this.gameObject;      
        HeadAnchor = PlayerObject.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").gameObject;
        PlayerLeftArmAnchor = PlayerObject.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor").gameObject;
        PlayerRightArmAnchor = PlayerObject.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/RightControllerAnchor").gameObject;

        nLife = PlayerData.nLife;
        nAttack = PlayerData.nAttack;
        nDefence = PlayerData.nDefence;
        fSpeed = PlayerData.fSpeed;
        
    }

    
}
