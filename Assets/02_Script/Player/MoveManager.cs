using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : PlayerStatus
{
    private CharacterController character;
    private Vector3 InitirizePos; // 正面リセット機能すると原点位置がリセットされる
    private Vector3 moveDirection;
    

    private float braketime = 1;
    private float gravity = -9.81f;
    private float fallingSpeed;

    public LayerMask groundLayer;
    public float braekpower;
   



    private void Start()
    {
        
        character = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        Gravity();

        HeadInclinationMove();
    }


    // プレイヤー移動
    void HeadInclinationMove()
    {
        // HMDのアンカーから移動量を取得
        Vector3 direction = new Vector3(headAnchor.transform.localPosition.x , 0, headAnchor.transform.localPosition.z );
        //Debug.Log("アンカーポイント：" + direction);

        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.LHandTrigger))
            {               
                //急ブレーキ
                braketime += Time.fixedDeltaTime * braekpower * 10;
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

    void Gravity()
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

    bool CheckIfGround()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }

    
}
