//============================================================
// PlayerData.cs
//======================================================================
// 開発履歴
//
// 
// 
//
//======================================================================
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create PlayerData")]

public class PlayerData : ScriptableObject
{
    [Header("プレイヤーのステータス")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;
}
