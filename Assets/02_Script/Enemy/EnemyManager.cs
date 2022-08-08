//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/15 author：ダメージを食らうように
// 
//
//======================================================================

using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : EnemyStatus, IDamageble<float>
{
    Animator Anim;
    NavMeshAgent Agent;
    [SerializeField] GameObject EnemyWeapon;
    public SoundManager SoundManager = null;
    [SerializeField] HPUI HPUI = null;
    [SerializeField]EnemyState CurrentState; 
    


    [Header("True=Gunner / False=Axe")]
    public bool bAttacktype = false;

    // プレイヤーの位置
    public Transform PlayerPos;

    

    private void Start()
    {
        EnemyObject = this.gameObject;
        Anim = EnemyObject.GetComponent<Animator>();
        Agent = EnemyObject.GetComponent<NavMeshAgent>();               
        SoundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();


        PlayerPos = GameObject.FindWithTag("Player").transform;
        CurrentState = new Idles(this.gameObject, Agent, Anim, PlayerPos, EnemyData); //Statesスクリプトの構造体Statesに当てはめて行動状態を宣言する
        
    }

    private void Update()
    {
        //CurrentState = CurrentState.Process();  //StatesのProcess関数を使用し、とるべき行動を呼び出す
        Debug.Log(CurrentState.name);
        
        HPUI.GaugeSlider((float)nLife / nMaxLife);

        if (nLife < 0)
        {
            HPUI.gameObject.SetActive(false);
            nLife = 0;
            bDead = true;
            CurrentState.isDeath = true;
        }
        
        
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            nLife = -1;
        }
    }

    public void AddDamage(float _damage)
    {
        //音を鳴らす
        SoundManager.Play_EnemyDamage(EnemyObject);
        nLife = nLife - (int)_damage;
    }

    

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
    //*****************************************************   
}
