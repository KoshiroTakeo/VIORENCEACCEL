//======================================================================
// EnemyWeapon.cs
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

public class EnemyWeapon : MonoBehaviour
{
    // 攻撃判定のオブジェクト、と名前
    GameObject AttackPoint = null;
    [SerializeField] string s_AtPointName = "AttackPointName";

    // 判定フレーム
    [SerializeField] int n_AtFlame = 0;

    // 攻撃開始
    bool b_Attack = false;
    int n_Count = 0;

    void Awake()
    {
        // 子オブジェクト取得
        AttackPoint = transform.Find("AttackErea").gameObject;
        AttackPoint.SetActive(false);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if(b_Attack)
        {
            
            n_Count++;
        }

        if (n_Count > n_AtFlame)
        {
            AttackPoint.SetActive(false);
            b_Attack = false;
            n_Count = 0;
        }
    }

    // 攻撃判定
    public void BeAttack()
    {
        AttackPoint.SetActive(true);
        b_Attack = true;

        
    }


    
}
