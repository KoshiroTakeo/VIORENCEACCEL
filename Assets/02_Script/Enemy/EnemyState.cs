//============================================================
// EnemyState.cs
//======================================================================
// 開発履歴
//
// 2022/05/31 author:竹尾　ポリモーフィズム確認
// 2022/06/01              MonoBehivior不要のため削除
// 2022/06/02              abstractにしてインスタンス不可にした
//
//======================================================================
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState // ルールとしてインスタンス不可にしてみた（これ単体では起動しないため）
{ 
    //行動状態の種類====================================================================
    public enum STATE // 列挙型
    {
        IDLE = 0,    // 待機
        PATROL = 1,  // 警備、徘徊
        PURSUE = 2,  // 追跡
        ATTACK = 3,  // 攻撃
        SLEEP = 4,   // 行動不能
        RUNAWAY = 5, // 逃走
        KNOCKOUT = 6,// 死亡時

        MAX
    };

    public STATE name; //行動状態確認用（書き換えられないようにしたい）
    //==================================================================================


    //イベント==========================================================================
    public enum EVENT
    {
        ENTER,  // 行動開始
        UPDATA, // 行動中
        EXIT    // 行動終了
    };

    protected EVENT stage;              
    //==================================================================================

    // 遷移用 ==========================================================================
    protected GameObject npc;           //対象キャラ
    protected Animator anim;            //アニメーション
    protected Transform player;         //プレイヤー座標
    protected NavMeshAgent agent;       //ナビメッシュ
    protected EnemyData enemyData;      //視認距離などの情報

    protected EnemyState nextEnemyState;//次の行動

    //上の各行動状態には、これらの変数を当てはめる
    public EnemyState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData)
    {
        npc = _npc;           //行動状態をとる対象()
        agent = _agent;       //対象の持つ（参照する）ナビゲーション
        anim = _anim;         //対象の持つ（参照する）アニメーション
        stage = EVENT.ENTER;  //状態開始時にとる行動
        player = _player;     //見ている対象、これに対して行動をとる
        enemyData = _enemyData;
        
    }
    //==================================================================================

    
    

    // 要らない ========================================================================
    public float WalkShift { get; set; } = 0.75f;            //歩行速度補正
    public bool isIdle { get; set; } = false;
    public bool isAttack { get; set; } = false;
    public bool isEndAnime { get; set; } = false;
    public bool isDeath { get; set; } = false;

    protected float fSmooth = 0;
    protected float isPlayAnimTime = 0;
    //==================================================================================




    // 行動開始、実行中、終了時の"最後に"それぞれ呼び出す
    public virtual void Enter()
    {
        fSmooth = 0;
        stage = EVENT.UPDATA; // ENTERだと止まる
    }  
    public virtual void Updata() 
    { 
        stage = EVENT.UPDATA;
        
    }  
    public virtual void Exit() 
    { 
        stage = EVENT.EXIT; 
    }   



    // これを呼び出す ********************************************
    public EnemyState Process()
    {
        //stateから読み取る
        if (stage == EVENT.ENTER) Enter();  //Enterの処理を行う

        if (stage == EVENT.UPDATA) Updata(); //Updataの処理を行う

        if (stage == EVENT.EXIT)      //Exitの処理を行う
        {
            Exit();
            return nextEnemyState; //次の行動に移す
        }

        return this; // EnemyStateを返す
    }
    //*************************************************************


    //アニメーション===============================================
    

    public void PlayAnimeSetFloat(string _parameters, float _force)
    {
        anim.SetFloat(_parameters, _force);

        isPlayAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //Debug.Log(isPlayAnimTime);
    }
    //=============================================================


    //*******************************************************************************************************************************************************************
    //　敵とプレイヤーの位置による行動決定関数=====================================================================
    //*******************************************************************************************************************************************************************
    //プレイヤー感知(true/falseで返す) --------------------
    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;  //directionでプレイヤーとの距離(位置)をとる
        float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す
        
        if (direction.magnitude < enemyData.visDist && angle < enemyData.visAngle) { return true; }; //距離が近く、指定の角度内にも存在するとき
        
        return false;
    }
    //-----------------------------------------------------

    //背後のプレイヤー感知(true/falseで返す) --------------
    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;  //directionで自身との距離(位置)をとる
        float angle = Vector3.Angle(direction, npc.transform.forward); //2点間の位置の角度を返す

        if (direction.magnitude <= enemyData.behideDist && angle < enemyData.behideAngle) { return true; };  //距離以内に、かつ背後にいたとき
        
        return false;
    }
    //-----------------------------------------------------

    //プレイヤーへ攻撃する(true/falseで返す) --------------
    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;  //directionで自身との距離(位置)をとる
        if (direction.magnitude < enemyData.shootDist) { return true; }                     //↑の位置が射程内であれば
        
        return false;
    }
    //-----------------------------------------------------

    public void NextState(EnemyState enemyState)
    {
        nextEnemyState = enemyState;
        stage = EVENT.EXIT;
    }
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
    public Idles(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData)
        : base(_npc, _agent, _anim, _player, _enemyData)                                              //baseで継承元(EnemyState)に実際にアクセスする
    {
        name = STATE.IDLE;                                                                //待機状態へ変更
    }


    //virtual修飾子が付いたEnterを用いる
    public override void Enter()                                                          //Enterメソッドの内容を利用して出力
    {
        agent.isStopped = true;
        base.Enter();                                                                     //Enter()から項目を継承
    }


    //virtual修飾子が付いたUpdataを用いる
    public override void Updata()                                                         //Updataメソッドの内容を利用して出力
    {
        PlayAnimeSetFloat("isMove", 0);

        if (isDeath) NextState(new Knockout(npc, agent, anim, player, enemyData));

        if (CanSeePlayer()) NextState(new Pursues(npc, agent, anim, player, enemyData));

        if (Random.Range(0, enemyData.BreakFrequency) < 10) NextState(new Patrols(npc, agent, anim, player, enemyData));
    }

    //virtual修飾子が付いたExitを用いる
    public override void Exit()                                                            //Exitメソッドの内容を利用して出力
    {
        base.Exit();
    }
}
//==================================================================================================================================================================



//徘徊行動==========================================================================================================================================================
public class Patrols : EnemyState                                                                //EnemyStateの機能を継承(スーパークラス)
{
    int currentIndex = -1;                                                                       //目標地点を数える
    public Patrols(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData) 
        : base(_npc, _agent, _anim, _player, _enemyData) //EnemyStateから４項目を継承
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

        agent.speed = enemyData.fSpeed * WalkShift;                                                                 //移動速度
        agent.isStopped = false;
        base.Enter();
    }

    public override void Updata()
    {
        if (isDeath) NextState(new Knockout(npc, agent, anim, player, enemyData));

        PlayAnimeSetFloat("isMove", Mathf.Lerp(0.0f, 0.5f, fSmooth += Time.deltaTime));

        if (agent.remainingDistance < 1)                                                                   //エージェントの位置および現在の経路での目標地点の間の距離
        {
            if (currentIndex >= EnemyWayPoint.Singleton.Checkpoints.Count - 1) currentIndex = 0;
            else currentIndex++;

            agent.SetDestination(EnemyWayPoint.Singleton.Checkpoints[currentIndex].transform.position);
        }

        if (CanSeePlayer()) NextState(new Pursues(npc, agent, anim, player, enemyData));
        else if (IsPlayerBehind()) NextState(new RunAways(npc, agent, anim, player, enemyData));
        
        // たまに休む
        //if (Random.Range(0, enemyData.BreakFrequency) < 10) NextState(new Idles(npc, agent, anim, player, enemyData));

    }



    public override void Exit()
    {
        base.Exit();
    }
}
//==================================================================================================================================================================


//追跡行動==========================================================================================================================================================
public class Pursues : EnemyState
{

    public Pursues(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData) : base(_npc, _agent, _anim, _player, _enemyData)
    {
        name = STATE.PURSUE;       
    }

    public override void Enter()
    {
        agent.speed = enemyData.fSpeed;
        agent.isStopped = false;
        base.Enter();
    }

    public override void Updata()
    {
        PlayAnimeSetFloat("isMove", Mathf.Lerp(0.5f, 1.0f, fSmooth += Time.deltaTime));

        if (isDeath) NextState(new Knockout(npc, agent, anim, player, enemyData));

        agent.SetDestination(player.position);

        if (agent.hasPath)
        {
            if (CanAttackPlayer()) NextState(new Attacks(npc, agent, anim, player, enemyData));
            else if (!CanSeePlayer()) NextState(new Idles(npc, agent, anim, player, enemyData));
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
//==================================================================================================================================================================


//攻撃行動==========================================================================================================================================================
public class Attacks : EnemyState
{
    float countInterval = 0;
    float rotationSpeed = 0.0f;


    public Attacks(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData)
        : base(_npc, _agent, _anim, _player, _enemyData)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        countInterval = 9999; //最初は攻撃させるため
        rotationSpeed = enemyData.Atk_Rotation;
        agent.isStopped = true;

        base.Enter();
    }

    public override void Updata()
    {
        if (isDeath) NextState(new Knockout(npc, agent, anim, player, enemyData));

        PlayAnimeSetFloat("isAttack", 1.0f);

        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        // 追っかけてほしい
        if (!CanAttackPlayer()) NextState(new Idles(npc, agent, anim, player, enemyData));

        countInterval += Time.deltaTime;

        if (countInterval > enemyData.Atk_Interbal)
        {
            Debug.Log("攻撃");
            isAttack = true;
            
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
    public RunAways(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData) : base(_npc, _agent, _anim, _player, _enemyData)
    {
        name = STATE.RUNAWAY;
    }

    public override void Enter()
    {
        safeLocation = GameObject.FindGameObjectWithTag("Safe");
        agent.speed = enemyData.fSpeed;
        agent.isStopped = false;
        agent.SetDestination(safeLocation.transform.position);
        
        base.Enter();
    }
    public override void Updata()
    {
        PlayAnimeSetFloat("isMove", 1.0f);

        if (isDeath) NextState(new Knockout(npc, agent, anim, player, enemyData));

        if (agent.remainingDistance < 1) NextState(new Idles(npc, agent, anim, player, enemyData));
        
       
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
    public Knockout(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyData _enemyData)
        : base(_npc, _agent, _anim, _player, _enemyData)                                              //baseで継承元(EnemyState)に実際にアクセスする
    {
        name = STATE.KNOCKOUT;                                                            //死亡状態へ変更
    }


    //virtual修飾子が付いたEnterを用いる
    public override void Enter()                                                          //Enterメソッドの内容を利用して出力
    {
        PlayAnimeSetFloat("isDeath", 1.0f);
        agent.isStopped = true;
        
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