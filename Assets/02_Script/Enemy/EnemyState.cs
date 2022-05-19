//============================================================
// EnemyState.cs
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
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    //行動状態の種類====================================================================
    public enum STATE //列挙型
    {
        IDLE,    // 待機
        PATROL,  // 警備、徘徊
        PURSUE,  // 追跡
        ATTACK,  // 攻撃
        SLEEP,   // 行動不能
        RUNAWAY, // 逃走
        KNOCKOUT,// 死亡時

        MAX
    };
    //==================================================================================


    //イベント==========================================================================
    public enum EVENT
    {
        ENTER,  // 行動開始
        UPDATA, // 行動中
        EXIT    // 行動終了
    };
    //==================================================================================



    public STATE name;                //行動状態宣言

    protected EVENT stage;              //
    protected GameObject npc;           //
    protected Animator anim;            //アニメーション
    protected Transform player;         //プレイヤー座標
    protected EnemyState nextEnemyState;//次の行動
    protected NavMeshAgent agent;       //ナビメッシュ



    public float visDist { get; set; } = 40.0f;             //検知距離
    public float visAngle { get; set; } = 60.0f;            //検知角
    public float shootDist { get; set; } = 30.0f;           //攻撃距離
    public float behideDist { get; set; } = 10.0f;           //背後距離
    public float behideAngle { get; set; } = 20.0f;         //背後角度
    public float Atk_Interbal { get; set; } = 3.0f;         //攻撃間隔
    public float Atk_Rotation { get; set; } = 5.0f;         //攻撃角度修正速度
    public float MoveSpeed { get; set; } = 10.0f;            //移動速度

    public float WalkShift { get; set; } = 0.75f;            //歩行速度補正
    public bool isIdle { get; set; } = false;
    public bool isAttack { get; set; } = false;
    public bool isEndAnime { get; set; } = false;
    public bool isDeath { get; set; } = false;


    protected float isPlayAnimTime = 0;


    //上の各行動状態には、これらの変数を当てはめる
    public EnemyState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;        //行動状態をとる対象()
        agent = _agent;      //対象の持つ（参照する）ナビゲーション
        anim = _anim;       //対象の持つ（参照する）アニメーション
        stage = EVENT.ENTER; //状態開始時にとる行動
        player = _player;     //見ている対象、これに対して行動をとる
    }



    public virtual void Enter() { stage = EVENT.UPDATA; Debug.Log("現在行動:" + name + "開始"); }  //利用できるようにvirtualを宣言
    public virtual void Updata() { stage = EVENT.UPDATA; Debug.Log("現在行動:" + name + "継続"); }  //利用できるようにvirtualを宣言
    public virtual void Exit() { stage = EVENT.EXIT; Debug.Log("現在行動:" + name + "終了"); }    //利用できるようにvirtualを宣言



    //行動変遷*****************************************************
    public EnemyState Process()
    {
        //stateから読み取る
        if (stage == EVENT.ENTER) Enter();  //Enterの処理を行う
        if (stage == EVENT.UPDATA) Updata(); //Updataの処理を行う
        if (stage == EVENT.EXIT)             //Exitの処理を行う
        {
            Exit();
            if(isDeath == true)
            {
                return new Knockout(npc, agent, anim, player); ; //次の行動に移す
            }
            else
            {
                return nextEnemyState; //次の行動に移す
            }
            
        }

        return this; // EnemyStateを返す
    }
    //*************************************************************

    // 各値の設定 *************************************************
    public void SetGuidInfo()
    {
        EnemyManager manager = npc.GetComponent<EnemyManager>();

        visDist = manager.visDist;
        visAngle = manager.visAngle;
        shootDist = manager.shootDist;
        behideDist = manager.behideDist;
        behideAngle = manager.behideAngle;
        Atk_Interbal = manager.Atk_Interbal;
        Atk_Rotation = manager.Atk_Rotation;
        MoveSpeed = manager.fSpeed;
        WalkShift = 0.75f;

        isDeath = manager.bDead;
        // いちいちセットするの重くない？ 20220516
    }

    //*************************************************************

    //アニメーション***********************************************
    public void PlayAnime()
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetFloat("isMove", agent.speed);
        anim.SetBool("isAttack", isAttack);
        anim.SetBool("Death", isDeath);
        isPlayAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

    }

    public void TriggerAnime_Death()
    {
        anim.SetTrigger("Death");
    }

    public void EndAnime() // アニメーションイベントで呼び出す
    {
        isEndAnime = true;
    }
    //*************************************************************


    //*******************************************************************************************************************************************************************
    //　敵とプレイヤーの位置による行動決定関数=====================================================================
    //*******************************************************************************************************************************************************************
    //プレイヤー感知(true/falseで返す) --------------------
    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;  //directionでプレイヤーとの距離(位置)をとる
        float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す

        

        if (direction.magnitude < visDist && angle < visAngle) { return true; }; //距離が近く、指定の角度内にも存在するとき
        
        return false;
    }
    //-----------------------------------------------------

    //背後のプレイヤー感知(true/falseで返す) --------------
    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;  //directionで自身との距離(位置)をとる
        float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す

        if (direction.magnitude <= behideDist && angle < behideAngle) { return true; };  //距離以内に、かつ背後にいたとき
        
        return false;
    }
    //-----------------------------------------------------

    //プレイヤーへ攻撃する(true/falseで返す) --------------
    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;  //directionで自身との距離(位置)をとる
        if (direction.magnitude < shootDist) { return true; }                     //↑の位置が射程内であれば
        
        return false;
    }
    //-----------------------------------------------------
    //==============================================================================================================
}










//*******************************************************************************************************************************************************************
//State内の各行動内容
// 
// Enterに開始の処理を書く
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
        isIdle = true;
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

        if (Random.Range(0, 50) < 10)                                                   //10/5000の確率で巡回状態へ
        {
            nextEnemyState = new Patrols(npc, agent, anim, player);                       //Patrols関数に持っている情報を渡し、巡回行動へ
            stage = EVENT.EXIT;                                                           //次の行動へ移すためにこの変数を送る
        }

        PlayAnime();
    }


    //virtual修飾子が付いたExitを用いる
    public override void Exit()                                                            //Exitメソッドの内容を利用して出力
    {
        isIdle = false;
        base.Exit();
    }
}
//==================================================================================================================================================================



//徘徊行動==========================================================================================================================================================
public class Patrols : EnemyState                                                                //EnemyStateの機能を継承(スーパークラス)
{
    int currentIndex = -1;                                                                       //目標地点を数える
    public Patrols(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) 
        : base(_npc, _agent, _anim, _player) //EnemyStateから４項目を継承
    {
        name = STATE.PATROL; //徘徊状態へ変更
        
    }
                                                                           


    public override void Enter()
    {
        float lastDist = Mathf.Infinity;                                                          //最初は距離を無限とする(すべてのチェックポイントを探知するため)
        
        for (int i = 0; i < EnemyWayPoint.Singleton.Checkpoints.Count; i++)                     //チェックポイントの数だけ周る
        {
            GameObject thisWP = EnemyWayPoint.Singleton.Checkpoints[i];                         //目的地を読み込む
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position); //目的地と自身の距離
            if (distance < lastDist)                                                              //たどり着いてなければ行う
            {
                currentIndex = i - 1;
                lastDist = distance;                                                              //無限の距離を次の目的地までとする
            }
        }
        agent.speed = MoveSpeed * WalkShift;                                                                 //移動速度
        //agent.isStopped = false;                                                                 //ナビゲーションを再開

        SetGuidInfo();

        base.Enter();
    }



    public override void Updata()
    {
        if (agent.remainingDistance < 1)                                                                   //エージェントの位置および現在の経路での目標地点の間の距離
        {
            if (currentIndex >= EnemyWayPoint.Singleton.Checkpoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(EnemyWayPoint.Singleton.Checkpoints[currentIndex].transform.position);
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

        PlayAnime();

    }



    public override void Exit()
    {
        //agent.speed = 0;
        base.Exit();
    }
}
//==================================================================================================================================================================


//追跡行動==========================================================================================================================================================
public class Pursues : EnemyState
{

    public Pursues(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player): base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;       
    }

    public override void Enter()
    {
        agent.speed = MoveSpeed;
        agent.isStopped = false;
        SetGuidInfo();
        base.Enter();
    }

    public override void Updata()
    {

        agent.SetDestination(player.position);
        //Debug.Log(agent.nextPosition);
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

        PlayAnime();
    }

    public override void Exit()
    {
        agent.speed = 0;
        base.Exit();
    }
}
//==================================================================================================================================================================


//攻撃行動==========================================================================================================================================================
public class Attacks : EnemyState
{
    float countInterval = 0;
    float rotationSpeed = 0.0f;


    public Attacks(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        countInterval = Atk_Interbal;
        rotationSpeed = Atk_Rotation;
        agent.isStopped = true;
        SetGuidInfo();
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

        countInterval += Time.deltaTime;

        //if (isEndAnime == true)
        //{
            
        //    isEndAnime = false;
            
        //}

        if (countInterval > Atk_Interbal)
        {
            isAttack = true;
            PlayAnime();
            isAttack = false;
            countInterval = 0;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
//==================================================================================================================================================================



//逃走行動==========================================================================================================================================================
public class RunAways : EnemyState
{
    GameObject safeLocation;
    public RunAways(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;
    }

    public override void Enter()
    {
        safeLocation = GameObject.FindGameObjectWithTag("Safe");
        agent.speed = MoveSpeed;
        agent.isStopped = false;
        agent.SetDestination(safeLocation.transform.position);
        SetGuidInfo();
        base.Enter();
    }
    public override void Updata()
    {
        if (agent.remainingDistance < 1)
        {
            nextEnemyState = new Idles(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        PlayAnime();
    }
    public override void Exit()
    {
        agent.speed = 0;

        base.Exit();
    }
}

//==================================================================================================================================================================

//死亡行動===========================================================================================================================================================
public class Knockout : EnemyState                                                        //EnemyStateの機能を継承(スーパークラス)し、EnemyStateでやれることをできるようにする
{
    public Knockout(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)                                              //baseで継承元(EnemyState)に実際にアクセスする
    {
        name = STATE.KNOCKOUT;                                                            //死亡状態へ変更
    }


    //virtual修飾子が付いたEnterを用いる
    public override void Enter()                                                          //Enterメソッドの内容を利用して出力
    {
        isDeath = true;
        TriggerAnime_Death();
        base.Enter();                                                                     //Enter()から項目を継承
    }


    //virtual修飾子が付いたUpdataを用いる
    public override void Updata()                                                         //Updataメソッドの内容を利用して出力
    {
        Debug.Log("死亡状態");

        
    }


    //virtual修飾子が付いたExitを用いる
    public override void Exit()                                                            //Exitメソッドの内容を利用して出力
    {
        
        base.Exit();
    }
}
//==================================================================================================================================================================