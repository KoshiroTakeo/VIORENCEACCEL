using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Intaractive
{


    public class EnemyState : MonoBehaviour
    {
        //行動状態の種類====================================================================
        public enum STATE //列挙型
        {
            IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
        };
        //==================================================================================


        //イベント==========================================================================
        public enum EVENT
        {
            ENTER, UPDATA, EXIT
        };
        //==================================================================================



        public STATE name;                //行動状態宣言

        protected EVENT stage;              //
        protected GameObject npc;           //
        protected Animator anim;            //アニメーション
        protected Transform player;         //プレイヤー座標
        protected EnemyState nextEnemyState;//次の行動
        protected NavMeshAgent agent;       //ナビメッシュ



        float visDist = 40.0f;            //検知距離
        float visAngle = 60.0f;            //検知角
        float shootDist = 20.0f;             //攻撃距離


        //アタック***********
        //protected BusterGun busterGun;
        protected float Atk_Interbal = 0;
        //*******************



        //上の各行動状態には、これらの変数を当てはめる
        public EnemyState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        {
            npc = _npc;        //行動状態をとる対象()
            agent = _agent;      //対象の持つ（参照する）ナビゲーション
            anim = _anim;       //対象の持つ（参照する）アニメーション
            stage = EVENT.ENTER; //状態開始時にとる行動
            player = _player;     //見ている対象、これに対して行動をとる
        }



        public virtual void Enter() { stage = EVENT.UPDATA; }  //利用できるようにvirtualを宣言
        public virtual void Updata() { stage = EVENT.UPDATA; }  //利用できるようにvirtualを宣言
        public virtual void Exit() { stage = EVENT.EXIT; }    //利用できるようにvirtualを宣言



        //行動変遷*****************************************************
        public EnemyState Process()
        {
            //stateから読み取る
            if (stage == EVENT.ENTER) Enter();  //Enterの処理を行う
            if (stage == EVENT.UPDATA) Updata(); //Updataの処理を行う
            if (stage == EVENT.EXIT)             //Exitの処理を行う
            {
                Exit();
                return nextEnemyState; //次の行動に移す
            }

            return this; // EnemyStateを返す
        }
        //*************************************************************


        //*******************************************************************************************************************************************************************
        //　敵とプレイヤーの位置による行動決定関数=====================================================================
        //*******************************************************************************************************************************************************************
        //プレイヤー感知(true/falseで返す)
        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - npc.transform.position;  //directionでプレイヤーとの距離(位置)をとる
            float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す

            if (direction.magnitude < visDist && angle < visAngle)         //距離が近く、指定の角度内にも存在するとき
            {

                return true;
            }

            return false;
        }

        //背後のプレイヤー感知(true/falseで返す)
        public bool IsPlayerBehind()
        {
            Vector3 direction = npc.transform.position - player.position;  //directionで自身との距離(位置)をとる
            float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す

            if (direction.magnitude <= 7 && angle < 30)                     //距離以内に、かつ背後にいたとき
            {

                return true;
            }
            return false;
        }

        //プレイヤーへ攻撃する(true/falseで返す)
        public bool CanAttackPlayer()
        {
            //Vector3 direction = npc.transform.position - player.position;
            Vector3 direction = player.position - npc.transform.position;  //directionで自身との距離(位置)をとる       
            if (direction.magnitude < shootDist)                           //↑の位置が射程内であれば
            {

                return true;
            }
            return false;
        }

        //==============================================================================================================
    }


    //*******************************************************************************************************************************************************************
    //State内の各行動内容
    //*******************************************************************************************************************************************************************
    //待機行動===========================================================================================================================================================
    public class Idles : EnemyState                                                                //EnemyStateの機能を継承(スーパークラス)し、EnemyStateでやれることをできるようにする
    {
        public Idles(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)                                              //baseで継承元(EnemyState)に実際にアクセスする
        {
            name = STATE.IDLE;                                                                //待機状態へ変更
        }


        //virtual修飾子が付いたEnterを用いる
        public override void Enter()                                                          //Enterメソッドの内容を利用して出力
        {
            anim.SetTrigger("isIdle");
            base.Enter();                                                                     //Enter()から項目を継承
        }


        //virtual修飾子が付いたUpdataを用いる
        public override void Updata()                                                         //Updataメソッドの内容を利用して出力
        {

            if (CanSeePlayer())                                                               //CanSeePlayer()がtrueになったら(プレーヤーをみつけたら)
            {
                nextEnemyState = new Pursues(npc, agent, anim, player);                       //Pursues関数に持っている情報を渡す
                stage = EVENT.EXIT;                                                           //次の行動へ移すためにこの変数を送る
            }

            if (Random.Range(0, 5000) < 10)                                                   //10/5000の確率で巡回状態へ
            {
                nextEnemyState = new Patrols(npc, agent, anim, player);                       //Patrols関数に持っている情報を渡し、巡回行動へ
                stage = EVENT.EXIT;                                                           //次の行動へ移すためにこの変数を送る
            }

        }


        //virtual修飾子が付いたExitを用いる
        public override void Exit()                                                            //Exitメソッドの内容を利用して出力
        {
            anim.ResetTrigger("isIdle");
            base.Exit();
        }
    }
    //==================================================================================================================================================================



    //徘徊行動==========================================================================================================================================================
    public class Patrols : EnemyState                                                                //EnemyStateの機能を継承(スーパークラス)
    {
        int currentIndex = -1;                                                                       //目標地点を数える
        public Patrols(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)                                                     //EnemyStateから４項目を継承
        {
            name = STATE.PATROL;                                                                     //徘徊状態へ変更
            agent.speed = 6;                                                                         //移動速度
            agent.isStopped = false;                                                                 //ナビゲーションを再開
        }



        public override void Enter()
        {
            float lastDist = Mathf.Infinity;                                                          //最初は距離を無限とする(すべてのチェックポイントを探知するため)
            for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)                     //チェックポイントの数だけ周る
            {
                GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];                         //目的地を読み込む
                float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position); //目的地と自身の距離
                if (distance < lastDist)                                                              //たどり着いてなければ行う
                {
                    currentIndex = i - 1;
                    lastDist = distance;                                                              //無限の距離を次の目的地までとする
                }
            }
            anim.SetTrigger("isWalking");

            base.Enter();
        }



        public override void Updata()
        {
            
            if (agent.remainingDistance < 1)                                                                   //エージェントの位置および現在の経路での目標地点の間の距離
            {
                if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;
                }

                agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
            }

            if (CanSeePlayer())
            {
                nextEnemyState = new Pursues(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (IsPlayerBehind())
            {
                nextEnemyState = new RunAways(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

        }



        public override void Exit()
        {
            anim.ResetTrigger("isWalking");
            base.Exit();
        }
    }
    //==================================================================================================================================================================


    //追跡行動==========================================================================================================================================================
    public class Pursues : EnemyState
    {

        public Pursues(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)
        {
            name = STATE.PURSUE;
            agent.speed = 15;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            anim.SetTrigger("isRunning");
            base.Enter();
        }

        public override void Updata()
        {

            agent.SetDestination(player.position);
            if (agent.hasPath)
            {

                if (CanAttackPlayer())
                {

                    nextEnemyState = new Attacks(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
                else if (!CanSeePlayer())
                {

                    nextEnemyState = new Idles(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
            }

        }

        public override void Exit()
        {
            anim.ResetTrigger("isRunning");
            base.Exit();
        }
    }
    //==================================================================================================================================================================


    //攻撃行動==========================================================================================================================================================
    public class Attacks : EnemyState
    {

        float rotationSpeed = 2.0f;


        public Attacks(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)
        {
            agent.speed = 0;
            name = STATE.ATTACK;


        }

        public override void Enter()
        {

            anim.SetTrigger("isShooting");
            agent.isStopped = true;

            //busterGun = GameObject.FindWithTag("EnemyGun").GetComponent<BusterGun>();

            base.Enter();
        }

        public override void Updata()
        {

            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y = 0;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (!CanAttackPlayer())
            {
                nextEnemyState = new Idles(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

            Atk_Interbal += Time.deltaTime;
            if (Atk_Interbal >= 1.2)
            {
                //busterGun.Fire();
                Atk_Interbal = 0;
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isShooting");
            //shoot.Stop();
            base.Exit();
        }
    }
    //==================================================================================================================================================================



    //逃走行動==========================================================================================================================================================
    public class RunAways : EnemyState
    {
        GameObject safeLocation;
        public RunAways(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)
        {
            name = STATE.RUNAWAY;
            safeLocation = GameObject.FindGameObjectWithTag("Safe");
        }

        public override void Enter()
        {
            anim.SetTrigger("isRunning");
            agent.isStopped = false;
            agent.speed = 6;
            agent.SetDestination(safeLocation.transform.position);
            base.Enter();
        }
        public override void Updata()
        {
            if (agent.remainingDistance < 1)
            {
                nextEnemyState = new Idles(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

        }
        public override void Exit()
        {
            anim.ResetTrigger("isRunning");

            base.Exit();
        }
    }
}
//==================================================================================================================================================================
