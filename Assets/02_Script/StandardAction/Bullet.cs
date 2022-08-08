//======================================================================
// Bullet.cs
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


public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Effect = null;
    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            Damage(collider.gameObject.GetComponent<EnemyManager>());            
        }
        
        Destroy(this.gameObject);
        
    }

    void Damage(IDamageble<float> damageble)
    {
        Debug.Log("ダメージ発生");
        damageble.AddDamage(10);
    }

}

