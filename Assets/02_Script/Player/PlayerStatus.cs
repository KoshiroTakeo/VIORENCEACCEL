using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
// プレイヤーのステータス、ゲームオブジェクト情報
//***********************************************

public class PlayerStatus : MonoBehaviour
{

    protected GameObject playerObject; 
    protected GameObject headAnchor;                                                   // 頭のトラッキング
    protected GameObject playerLeftArmAnchor, playerRightArmAnchor;                    // 両手のトラッキング
    protected GameObject L_FirstWeapon, L_SecondWeapon, R_FirstWeapon, R_SecondWeapon; // 装備された武器
    

    [Header("ステータス")]
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
