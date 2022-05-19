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
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(Effect);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
}

