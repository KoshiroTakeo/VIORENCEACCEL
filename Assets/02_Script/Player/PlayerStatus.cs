using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
// �v���C���[�̃X�e�[�^�X�A�Q�[���I�u�W�F�N�g���
//***********************************************

public class PlayerStatus : MonoBehaviour
{

    protected GameObject playerObject; 
    protected GameObject headAnchor;                                                   // ���̃g���b�L���O
    protected GameObject playerLeftArmAnchor, playerRightArmAnchor;                    // ����̃g���b�L���O
    protected GameObject L_FirstWeapon, L_SecondWeapon, R_FirstWeapon, R_SecondWeapon; // �������ꂽ����
    

    [Header("�X�e�[�^�X")]
    public int life = 10;
    public int attack = 10;
    public int defence = 10;
    public float speed = 10;

    private void Awake()
    {
        playerObject = this.gameObject;
        headAnchor = playerObject.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").gameObject;
        playerLeftArmAnchor = playerObject.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor").gameObject;
        playerRightArmAnchor = playerObject.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/RightControllerAnchor").gameObject;
        
      
    }

    
}
