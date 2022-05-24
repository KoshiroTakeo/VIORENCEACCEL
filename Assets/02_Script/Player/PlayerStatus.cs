//============================================================
// PlayerStatus.cs
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



public class PlayerStatus : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    protected GameObject PlayerObject; 
    protected GameObject HeadAnchor;                                                   // 頭のトラッキング
    protected GameObject PlayerLeftArmAnchor, PlayerRightArmAnchor;                    // 両手のトラッキング
    protected GameObject L_FirstWeapon, L_SecondWeapon, R_FirstWeapon, R_SecondWeapon; // 装備された武器
    

    [Header("ステータス")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;
    //public float fHeight = 1.8f; //プレイヤーの身長を設定

    [Header("状態")]
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
