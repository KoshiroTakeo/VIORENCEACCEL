//============================================================
// HPUI.cs
//======================================================================
// �J������
//
// 2022/04/29 author:�|���@����
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField]GameObject GaugeMain = null;

    GameObject PlayerObj = null;
    
    Vector3 MaxScale = new Vector3();

    private void Start()
    {
        PlayerObj = GameObject.FindWithTag("Player");
        MaxScale = GaugeMain.transform.localScale;
    }

    private void Update()
    {
        //var aim = PlayerObj.transform.position - this.gameObject.transform.position;
        //var look = Quaternion.LookRotation(aim);
        //this.transform.localRotation = look;

        this.transform.LookAt(PlayerObj.transform);

        //Debug.Log(PlayerObj.transform.position);
    }

    // �Q�[�W�̑������� =================================================================
    public void GaugeSlider(float percent)
    {
        GaugeMain.transform.localScale = new Vector3(MaxScale.x * percent, MaxScale.y, MaxScale.z);
    }
    //===================================================================================

   
}
