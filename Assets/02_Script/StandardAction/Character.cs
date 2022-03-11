using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace StandardAction
{
    // あらゆる動く生物のの基盤
    public class Character :MonoBehaviour 
    {
        
        private CharacterController characterController;
        private GameObject myOwnObj;

        [SerializeField]  protected Animator animator;
        protected NavMeshAgent navMeshAgent;
        protected Vector3 velocity;

        [SerializeField] public GameObject mazule; //探索して追加したい
        [SerializeField] public GameObject bullet;
        

        [Header("ステータス")]
        protected int characterLevel = 1;
        protected int hp = 1;
        protected int atk = 1;
        protected int def = 1;
        protected float speed = 1;
        protected float jumpPower = 1;
        protected float bulletSpeed = 1;

        private float bulletDeleatTime = 8.0f;

        private void Awake()
        {
            myOwnObj = this.gameObject;

            SetComponent();
            
        }




        // 移動
        public void Move(Vector2 vector2, bool isJump)
        {
            if (characterController.isGrounded)
            {
                velocity = Vector3.zero;

                var input = new Vector3(vector2.x, 0, vector2.y);

                if (input.magnitude > 0f)
                {
                    transform.LookAt(transform.position + input);
                    velocity = transform.forward * speed;
                    //animator.SetFloat("Speed", input.magnitude);
                }
                else
                {
                    //animator.SetFloat("Speed", 0);
                }

                if (isJump == true)
                {
                    velocity.y += jumpPower;
                }

            }

            velocity.y += Physics.gravity.y * Time.deltaTime;

            characterController.Move(velocity * Time.fixedDeltaTime * speed);

        }





        // コンポーネント追加
        void SetComponent()
        {
            if (myOwnObj.GetComponent<CharacterController>() == null)
            { 
                characterController = myOwnObj.AddComponent<CharacterController>();
                
            }

        }



        // 攻撃
        public void Fire(GameObject mazzle,float bulletSpeed, GameObject bullet, string tagName, bool isAttack)
        {
            if (isAttack == true)
            {
                GameObject newBullet = Instantiate(bullet, mazule.transform.position, transform.rotation);
                Vector3 direction = mazule.transform.forward;
                newBullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
                newBullet.GetComponent<Bullet>().BulletStatus(atk, tagName);
                newBullet.name = bullet.name;

                Destroy(newBullet, bulletDeleatTime);
            }
        }



        // ダメージ計算
        public void IsDamage(int bulletAtk)
        {
            hp -= bulletAtk;
        }


        // 消滅処理
        public void IsDestroy(int num)
        {
            if(num <= 0)
            {              
                Destroy(this.gameObject);
            }
        }
        


       
    }
}

