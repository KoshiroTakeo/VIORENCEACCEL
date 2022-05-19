//============================================================
// GeneraterList.cs
//======================================================================
// 開発履歴
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
    [Header("敵リスト")]
    public List<GameObject> EnemyList = new List<GameObject>();

    [Header("生成場所リスト")]
    public List<GameObject> SpawnPos = new List<GameObject>();

    [Header("最大生成数")]
    public int MaxEnemy = 30;
    
}
