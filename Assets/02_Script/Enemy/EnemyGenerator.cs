//======================================================================
// EnemyManager.cs
//======================================================================
// �J������
//
// 2022/05/06 author�F�|���@��������������
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // ���ݏo�����Ă���G
    [Header("���ݏo�����Ă���G")]
    [SerializeField] List<GameObject> GenerateList = new List<GameObject>();
    [SerializeField] GeneraterList GeneraterData;
    List<GameObject> EnemyList;    // �o��������G
    List<GameObject> SpawnPosList; // �o��������ʒu

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

    // ���Ԍv�� ************************************************
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

    // �G�̐��� ************************************************
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
