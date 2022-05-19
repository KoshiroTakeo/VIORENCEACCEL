//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/05/06 author：竹尾　生成処理見直し
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // 現在出現している敵
    [Header("現在出現している敵")]
    [SerializeField] List<GameObject> GenerateList = new List<GameObject>();
    [SerializeField] GeneraterList GeneraterData;
    List<GameObject> EnemyList;    // 出現させる敵
    List<GameObject> SpawnPosList; // 出現させる位置

    public int n_GenaratorNum = 0;
    float n_Intarbal = 5.0f;
    float Count = 0;

    int EnemyCount = 0;
    int MaxEnemy = 0;


    private void Start()
    {
        EnemyList = GeneraterData.EnemyList;
        SpawnPosList = GeneraterData.SpawnPos;

        MaxEnemy = GeneraterData.MaxEnemy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Timer())
        {
            GenarateEnemy();
        }
    }

    // 時間計測 ************************************************
     bool Timer()
    {
        Count += Time.deltaTime;

        if(Count > n_Intarbal) 
        {
            Count = 0;
            return true;
        }
        else 
        {
            return false;
        }
    }
    //**********************************************************

    // 敵の生成 ************************************************
    void GenarateEnemy()
    {
        if(EnemyCount < MaxEnemy)
        {
            GameObject setenemy = EnemyList[Random.Range(0, EnemyList.Count)];
            Vector3 setspawnpos = SpawnPosList[Random.Range(0, SpawnPosList.Count)].transform.position;

            GenerateList.Add(Instantiate(setenemy, setspawnpos, Quaternion.identity));
            EnemyCount++;
        }

        
            for (int i = 0; i < GenerateList.Count; i++)
            {
                if (GenerateList[i] == null)
                {
                    GenerateList.Remove(GenerateList[i]);
                }
            }
        
    }
    //**********************************************************
}
