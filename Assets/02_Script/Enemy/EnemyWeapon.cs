//======================================================================
// EnemyWeapon.cs
//======================================================================
// �J������
//
// 2022/03/15 author�F�_���[�W��H�炤�悤��
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // �U������̃I�u�W�F�N�g�A�Ɩ��O
    GameObject AttackPoint = null;
    [SerializeField] string s_AtPointName = "AttackPointName";

    // ����t���[��
    [SerializeField] int n_AtFlame = 0;

    // �U���J�n
    bool b_Attack = false;
    int n_Count = 0;

    void Awake()
    {
        // �q�I�u�W�F�N�g�擾
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

    // �U������
    public void BeAttack()
    {
        AttackPoint.SetActive(true);
        b_Attack = true;

        
    }


    
}
