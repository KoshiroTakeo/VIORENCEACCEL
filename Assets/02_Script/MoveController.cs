using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveController : MonoBehaviour
{
    //OVRManager使用********************************************
    OVRInput.Controller RightCon; //右コントローラーの情報を取得
    OVRInput.Controller LeftCon;  //左コントローラーの情報を取得
    //**********************************************************

    private GameObject HMDAnchor;
    private CharacterController character;
    private Vector3 InitirizePos;
    private Vector3 moveDirection;

    private float speed = 10;
    private float braketime = 1;
    private float gravity = -9.81f;
    private float fallingSpeed;

    public LayerMask groundLayer;


    //武装関連
    //public GameObject LeftGun_obj;
    //public GameObject RightGun_obj;
    //public GameObject LeftBlead_obj;
    //public GameObject RightBlead_obj;
    //float interbal_L = 0;
    //float interbal_R = 0;
    //bool WeaponChange_L = false;
    //bool WeaponChange_R = false;

    //HP表記
    //public Slider slider;
    //public int MaxHP_test = 100;
    //int Hp_test;

    void Start()
    {
        RightCon = OVRInput.Controller.RTouch;
        LeftCon = OVRInput.Controller.LTouch;

        HMDAnchor = transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").gameObject;
        character = GetComponent<CharacterController>();        
        InitirizePos = HMDAnchor.transform.position;
        
        

        //slider.value = 1;
        //Hp_test = MaxHP_test;
    }

    
    void Update()
    {
        Gravity();

        HeadInclinationMove();

        //Action();
    }

    void FixedUpdate()
    {
        
        

    }

    private void LateUpdate()
    {
        
    }


    //HMDの位置で移動するようにする******************************************************************************
    void HeadInclinationMove()
    {
        Vector3 direction = new Vector3(HMDAnchor.transform.localPosition.x * 2, 0, HMDAnchor.transform.localPosition.z * 2);

        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.LHandTrigger))
            {
                //急ブレーキ
                braketime += Time.fixedDeltaTime * 30.0f;
            }
            else
            {
                //ブレーキ
                braketime += Time.fixedDeltaTime * 3.0f;
            }
         
            character.Move(direction * Time.fixedDeltaTime * (speed / braketime));
        }
        else
        {
            braketime = 1;
            character.Move(direction * Time.fixedDeltaTime * speed);
        }

        ////ブースター機構
        //if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        //{

        //    //ブレーキ
        //    braketime += Time.fixedDeltaTime;
        //    if (braketime >= 4) braketime = 4.0f;
        //    character.Move(direction * Time.fixedDeltaTime * (speed / braketime));


        //}


    }
    //**********************************************************************************************************


    //重力**********************************************************************
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
    //**************************************************************************

    
    //地面との接触判定**********************************************************
    bool CheckIfGround()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
    //**************************************************************************

    
    //アクション********************************************************************
    //void Action()
    //{
    //    if (WeaponChange_L == false)
    //    {
    //        LeftGun_obj.SetActive(!WeaponChange_L);
    //        LeftBlead_obj.SetActive(WeaponChange_L);

    //        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
    //        {

    //            interbal_L += Time.deltaTime;
    //            if (interbal_L >= 0.3)
    //            {
    //                //LeftGun.Fire();
    //                interbal_L = 0;
    //            }

    //        }
    //    }
    //    else
    //    {
    //        RightGun_obj.SetActive(!WeaponChange_L);
    //        RightBlead_obj.SetActive(WeaponChange_L);
    //    }



    //    if (WeaponChange_R == false)
    //    {
    //        RightGun_obj.SetActive(!WeaponChange_R);
    //        RightBlead_obj.SetActive(WeaponChange_R);

    //        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
    //        {
    //            interbal_R += Time.deltaTime;
    //            if (interbal_R >= 0.3)
    //            {
    //                //RightGun.Fire();
    //                interbal_R = 0;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        RightGun_obj.SetActive(!WeaponChange_R);
    //        RightBlead_obj.SetActive(WeaponChange_R);
    //    }
        

    //    if (OVRInput.GetDown(OVRInput.RawButton.X))
    //    {
    //        WeaponChange_L = !WeaponChange_L;
    //    }

    //    if (OVRInput.GetDown(OVRInput.RawButton.A))
    //    {
    //        WeaponChange_R = !WeaponChange_R;
    //    }
    //}
    //***************************************************************************

    //ColliderオブジェクトのIsTriggerにチェック入れること。
    private void OnTriggerEnter(Collider collider)
    {
        //Enemyタグのオブジェクトに触れると発動
        if (collider.gameObject.tag == "Bullet")
        {
            //ダメージは1～100の中でランダムに決める。
            int damage = Random.Range(1, 100);
            Debug.Log("damage : " + damage);

            //現在のHPからダメージを引く
            //Hp_test = Hp_test - damage;
            //Debug.Log("After Hp_test : " + Hp_test);

            //最大HPにおける現在のHPをSliderに反映。
            //int同士の割り算は小数点以下は0になるので、
            //(float)をつけてfloatの変数として振舞わせる。
            //slider.value = (float)Hp_test / (float)MaxHP_test; ;
            //Debug.Log("slider.value : " + slider.value);
        }
    }
}
