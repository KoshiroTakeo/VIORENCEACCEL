//============================================================
// ActionManager.cs
//======================================================================
// �J������
//
// 2022/03/15 author�F�_���[�W��H�炤�悤�ɁA���ꂢ�ɂ�������
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public GameObject FirstWeapon_L, SecondWeapon_L, FirstWeapon_R, SecondWeapon_R;
    private bool switch_R, switch_L;

    private void Start()
    {
        switch_R = switch_L = false;
        //*******************************************************
        // �������ꂽ���킩��Q��
        
        // L_FirstWeapon, L_SecondWeapon, R_FirstWeapon, R_SecondWeapon �ɂ����X�N���v�g���Q��
        //
        //*******************************************************

        LeftChangeWepon();
        RightChangeWepon();

    }

    private void Update()
    {
        
    }

    // �e����̌ŗL�A�N�V�����g���K�[***********
    public void Attack_L()
    {
        if (switch_L)
        {
            FirstWeapon_L.GetComponent<GunWeapon>().Fire();
        }
        else
        {
            SecondWeapon_L.GetComponent<GunWeapon>().Fire();
        }
    }

    public void Attack_R()
    {
        if (switch_R)
        {
            FirstWeapon_R.GetComponent<GunWeapon>().Fire();
        }
        else
        {
            SecondWeapon_R.GetComponent<GunWeapon>().Fire();
        }
    }
    //*********************************************

    // ���튷�� ***********************************
    public void LeftChangeWepon()
    {
        //Debug.Log("Left_WepaonChange");
        switch_L = !switch_L;

        FirstWeapon_L.SetActive(switch_L);
        SecondWeapon_L.SetActive(!switch_L);
    }

    public void RightChangeWepon()
    {
        //Debug.Log("Right_WepaonChange");
        switch_R = !switch_R;

        FirstWeapon_R.SetActive(switch_R);
        SecondWeapon_R.SetActive(!switch_R);
    }
    //**********************************************

    
}
