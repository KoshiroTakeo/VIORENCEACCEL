//============================================================
// MoveManager.cs
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

public class MoveManager : MonoBehaviour
{
    private CharacterController character;
    private Vector3 InitirizePos; // 正面リセット機能すると原点位置がリセットされる
    private Vector3 moveDirection;
    

    private float braketime = 1;
    private float gravity = -9.81f;
    private float fallingSpeed;



    public LayerMask groundLayer;
    public float braekpower;
    private float fHighPower = 10;


    public OVRCameraRig rig;
    public float additionalHeight = 0.2f;




    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    
    // プレイヤー移動 =========================================================
    public void HeadInclinationMove(GameObject anchor, float speed, Vector3 setPos)
    {
        Vector3 vector = anchor.transform.position - setPos; // 常に今いる位置を原点とする

        // 最高速度
        float fAnchorX = vector.x;
        if (fAnchorX > 2) fAnchorX = 2;

        float fAnchorZ = vector.z;
        if (fAnchorZ > 2) fAnchorZ = 2;

        // anchorから渡される、HMDのアンカーから移動量を取得
        Vector3 direction = new Vector3(fAnchorX, 0, fAnchorZ);
        //direction = transform.TransformDirection(direction); // ローカル座標からワールド座標へ

        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.LHandTrigger))
            {               
                //急ブレーキ
                braketime += Time.fixedDeltaTime * braekpower * fHighPower;
            }
            else
            {
                //ブレーキ
                braketime += Time.fixedDeltaTime * braekpower;
            }

            character.Move(direction * Time.fixedDeltaTime * (speed / braketime));
        }
        else
        {
            braketime = 1;
            character.Move(direction * Time.fixedDeltaTime * speed);
        }

        
    }
    //=========================================================================

    // 重力 ===================================================================
    public void Gravity()
    {
        bool isGrounded = CheckIfGround();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
    //=========================================================================

    // 接地判定 ===============================================================
    bool CheckIfGround()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
    //=========================================================================

    // 頭の位置によりコライダーを変形させる =====================================
    public void CapsuleFollowHeadset()
    {
        //Debug.Log(additionalHeight);
        character.height = rig.centerEyeAnchor.position.y / 2 + additionalHeight;
        //Vector3 capsuleCenter = transform.InverseTransformPoint(rig.centerEyeAnchor.transform.position);
        character.center = new Vector3(character.center.x, character.height / 2 + character.skinWidth, character.center.z);
    }
    //=========================================================================

    //cameraInRigSpaceHeight
}
