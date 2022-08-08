//============================================================
// EnemyStatus.cs
//======================================================================
// ŠJ”­—š—ğ
//
// 
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] public EnemyData EnemyData = null;
    protected GameObject EnemyObject = null;

    [Header("ƒXƒe[ƒ^ƒX")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;

    protected int nMaxLife = 0;

    public float visDist { get; set; } = 40.0f;            //ŒŸ’m‹——£
    public float visAngle { get; set; }  = 60.0f;            //ŒŸ’mŠp
    public float shootDist { get; set; } = 30.0f;             //UŒ‚‹——£
    public float behideAngle { get; set; } = 20.0f;             //”wŒãŠp“x
    public float behideDist { get; set; } = 10.0f;             //”wŒãŠp“x
    public float Atk_Interbal { get; set; } = 5.0f;             //UŒ‚ŠÔŠu
    public float Atk_Rotation { get; set; } = 5.0f;             //UŒ‚Šp“xC³‘¬“x
    public int BreakFrequency { get; set; } = 1000;                   //‘Ò‹@ó‘Ô‚Ö–ß‚é•p“x



    [Header("ó‘Ô")]
    public bool bDead = false;

    private void Awake()
    {
        EnemyObject = this.gameObject;

        nMaxLife = nLife = EnemyData.nLife;
        nAttack = EnemyData.nAttack;
        nDefence = EnemyData.nDefence;
        fSpeed = EnemyData.fSpeed;

        visDist = EnemyData.visDist;
        visAngle = EnemyData.visAngle;
        shootDist = EnemyData.shootDist;
        behideAngle = EnemyData.behideAngle;
        behideDist = EnemyData.behideDist;
        Atk_Interbal = EnemyData.Atk_Interbal;
        Atk_Rotation = EnemyData.Atk_Rotation;
        BreakFrequency = EnemyData.BreakFrequency;


        
    }
}
