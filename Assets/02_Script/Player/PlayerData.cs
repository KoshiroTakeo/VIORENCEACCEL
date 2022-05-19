//============================================================
// PlayerData.cs
//======================================================================
// �J������
//
// 
// 
//
//======================================================================
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create PlayerData")]

public class PlayerData : ScriptableObject
{
    [Header("�v���C���[�̃X�e�[�^�X")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;
}
