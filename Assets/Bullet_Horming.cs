//======================================================================
// Bullet_Horming.cs
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

public class Bullet_Horming : MonoBehaviour
{
    [SerializeField] GameObject Effect = null;
    Rigidbody rigid;             //Rigidbodyを入れる変数
    Vector3 velocity;            //速度    
    Vector3 position;            //発射するときの初期位置
    public Vector3 acceleration; // 加速度
    public Transform target;     // ターゲットをセットする
    public float period = 2f;           // 着弾時間

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        position = transform.position; 
        rigid = this.GetComponent<Rigidbody>();

    }

    void Update()
    {

        acceleration = Vector3.zero;

        //ターゲットと自分自身の差
        var diff = target.position - transform.position;

        //加速度を求めてるらしい
        acceleration += (diff - velocity * period) * 2f
                        / (period * period);


        //加速度が一定以上だと追尾を弱くする
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        // 着弾時間を徐々に減らしていく
        period -= Time.deltaTime;

        // 速度の計算
        velocity += acceleration * Time.deltaTime;

    }

    void FixedUpdate()
    {
        // 移動処理
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject effect = Instantiate(Effect);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
}
