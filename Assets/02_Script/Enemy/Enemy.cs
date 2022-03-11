//======================================================================
// Enemy.cs
//======================================================================
// 開発履歴
//
// 2022/03/08 author：
// 
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Intaractive
{


    public class Enemy : MonoBehaviour
    {
        [SerializeField]GameObject EnemyObj;
        Animator Anim;
        Rigidbody RB;
        NavMeshAgent Agent;

        public Transform PlayerPos;

        EnemyState currentState; //EnemyStateスクリプトを使用


        public float power;
        bool DicideAI;
        bool Animend;
        bool WeakTime;



        //テスト用
        public Slider slider;
        public int MaxHP_test = 100;
        int Hp_test;






        private void Start()
        {
            EnemyObj = this.gameObject;
            Anim = EnemyObj.GetComponent<Animator>();
            RB = EnemyObj.GetComponent<Rigidbody>();
            Agent = EnemyObj.GetComponent<NavMeshAgent>();               //ナビゲーション使用のため


            PlayerPos = GameObject.FindWithTag("Player").transform;
            currentState = new Idles(this.gameObject, Agent, Anim, PlayerPos); //Statesスクリプトの構造体Statesに当てはめて行動状態を宣言する
            DicideAI = false;


            slider.value = 1;
            Hp_test = MaxHP_test;
        }

        private void Update()
        {
            currentState = currentState.Process();  //StatesのProcess関数を使用し、とるべき行動を呼び出す
            Debug.Log(currentState.Process().name);

        }

        private void FixedUpdate()
        {


        }

        //攻撃を受けたとき***********************************
        void Reaction()
        {

        }
        //*****************************************************





        //あたり判定*********************************************
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                Debug.Log("Hit!");
                Anim.SetTrigger("Damage");
                RB.velocity = RB.velocity * power;
            }
        }
        //*************************************************



        //アニメ終了通知************************************
        public void EndAnime()
        {
            //Debug.Log("次の行動");
            DicideAI = false;
        }
        //**************************************************



        //ColliderオブジェクトのIsTriggerにチェック入れること。
        private void OnTriggerEnter(Collider collider)
        {
            //Enemyタグのオブジェクトに触れると発動
            if (collider.gameObject.tag == "Bullet")
            {
                //ダメージは1～100の中でランダムに決める。
                int damage = Random.Range(1, 100);


                //現在のHPからダメージを引く
                Hp_test = Hp_test - damage;


                //最大HPにおける現在のHPをSliderに反映。
                //int同士の割り算は小数点以下は0になるので、
                //(float)をつけてfloatの変数として振舞わせる。
                slider.value = (float)Hp_test / (float)MaxHP_test; ;

            }
        }
    }
}