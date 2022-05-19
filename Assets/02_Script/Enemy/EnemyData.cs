//============================================================
// EnemyData.cs
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

[CreateAssetMenu(menuName = "Scriptable/Create EnemyData")]

public class EnemyData : ScriptableObject
{ 
    [Header("“G‚ÌƒXƒe[ƒ^ƒX")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;

    [Header("ŒŸ’m‹——£")]
    public float visDist = 40.0f;            //ŒŸ’m‹——£
    [Header("‹–ìŠp")]
    public float visAngle = 60.0f;            //ŒŸ’mŠp
    [Header("UŒ‚‹——£")]
    public float shootDist = 30.0f;             //UŒ‚‹——£
    [Header("”wŒãŠp“x")]
    public float behideAngle = 20.0f;             //”wŒãŠp“x
    [Header("”wŒã‹——£")]
    public float behideDist = 10.0f;             //”wŒãŠp“x
    [Header("UŒ‚ŠÔŠu")]
    public float Atk_Interbal = 5.0f;             //UŒ‚ŠÔŠu
    [Header("UŒ‚Šp“xC³‘¬“x")]
    public float Atk_Rotation = 5.0f;             //UŒ‚Šp“xC³‘¬“x

    public enum EnemyType
    {
        Normal_Axe,
        Normal_Gunner,

        MAX
    }
}
