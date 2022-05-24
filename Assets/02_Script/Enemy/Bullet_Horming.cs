//======================================================================
// Bullet_Horming.cs
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

public class Bullet_Horming : MonoBehaviour
{
    [SerializeField] GameObject Effect = null;
    Rigidbody rigid;             //Rigidbody������ϐ�
    Vector3 velocity;            //���x    
    Vector3 position;            //���˂���Ƃ��̏����ʒu
    public Vector3 acceleration; // �����x
    public Transform target;     // �^�[�Q�b�g���Z�b�g����
    public float period = 2f;           // ���e����

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        position = transform.position; 
        rigid = this.GetComponent<Rigidbody>();

    }

    void Update()
    {

        acceleration = Vector3.zero;

        //�^�[�Q�b�g�Ǝ������g�̍�
        var diff = target.position - transform.position;

        //�����x�����߂Ă�炵��
        acceleration += (diff - velocity * period) * 2f
                        / (period * period);


        //�����x�����ȏゾ�ƒǔ����キ����
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        // ���e���Ԃ����X�Ɍ��炵�Ă���
        period -= Time.deltaTime;

        // ���x�̌v�Z
        velocity += acceleration * Time.deltaTime;

    }

    void FixedUpdate()
    {
        // �ړ�����
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject effect = Instantiate(Effect);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
}
