//============================================================
// GeneraterList.cs
//======================================================================
// �J������
//
// 
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create GenerateData")]

public class GeneraterList : ScriptableObject
{
    [Header("�G���X�g")]
    public List<GameObject> EnemyList = new List<GameObject>();

    [Header("�����ꏊ���X�g")]
    public List<GameObject> SpawnPos = new List<GameObject>();

    [Header("�ő吶����")]
    public int MaxEnemy = 30;
    
}
