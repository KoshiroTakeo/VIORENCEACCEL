//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/15 author：ダメージを食らうように
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : EnemyStatus
{
    Animator Anim;
    //Rigidbody RB;
    NavMeshAgent Agent;
    [SerializeField] GameObject EnemyWeapon;
    public SoundManager SoundManager = null;
    [SerializeField] HPUI HPUI = null;
    EnemyState CurrentState; 

    [Header("True=Gunner / False=Axe")]
    public bool bAttacktype = false;

    // プレイヤーの位置
    public Transform PlayerPos;

    

    private void Start()
    {
        EnemyObject = this.gameObject;
        Anim = EnemyObject.GetComponent<Animator>();
        //RB = EnemyObject.GetComponent<Rigidbody>();
        Agent = EnemyObject.GetComponent<NavMeshAgent>();               //ナビゲーション使用のため
        SoundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();


        PlayerPos = GameObject.FindWithTag("Player").transform;
        CurrentState = new Idles(this.gameObject, Agent, Anim, PlayerPos); //Statesスクリプトの構造体Statesに当てはめて行動状態を宣言する
        SetSerchPerformance();

        
    }

    private void Update()
    {
        HPUI.GaugeSlider((float)nLife / nMaxLife);

        if (nLife < 0)
        {
            HPUI.gameObject.SetActive(false);
            nLife = 0;
            bDead = true;
        }

        CurrentState = CurrentState.Process();  //StatesのProcess関数を使用し、とるべき行動を呼び出す

        

       
    }



    // ダメージ判定 **************************************
    void IsDamage()
    {
        //音を鳴らす
        SoundManager.Play_EnemyDamage(EnemyObject);

        //ダメージは1～100の中でランダムに決める。
        int damage = Random.Range(3, 10);


        //現在のHPからダメージを引く
        nLife = nLife - damage;

    }

    
    //*****************************************************

    // アニメーションイベント用****************************
    void AttackEvent()
    {
        if (bAttacktype == false)
        {
            EnemyWeapon.GetComponent<EnemyWeapon>().BeAttack();
        }
        else
        {
            EnemyWeapon.GetComponent<Enemy_Shoot>().BeAttack();
        }
           
    }

     public void AnimeEndEvent()
    {
        
        CurrentState.EndAnime();
    }
    //*****************************************************


    // あたり判定 *****************************************
    private void OnTriggerEnter(Collider collider)
    {
        //Enemyタグのオブジェクトに触れると発動
        if (collider.gameObject.tag == "PlayerAttack" && bDead == false)
        {
            IsDamage();
        }
    }
    //*****************************************************

    // 索敵数値設定 ***************************************
    void SetSerchPerformance()
    {
        CurrentState.visDist = EnemyData.visDist;
        CurrentState.visAngle = EnemyData.visAngle;
        CurrentState.shootDist = EnemyData.shootDist;
        CurrentState.behideAngle = EnemyData.behideAngle;
        CurrentState.behideDist = EnemyData.behideDist;
        CurrentState.Atk_Interbal = EnemyData.Atk_Interbal;
        CurrentState.Atk_Rotation = EnemyData.Atk_Rotation;
        CurrentState.MoveSpeed = EnemyData.fSpeed;

    }
    //*****************************************************
}
