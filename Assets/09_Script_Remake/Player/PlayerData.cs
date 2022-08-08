//============================================================
// �v���C���[�f�[�^�̃X�N���^�u���f�[�^
//======================================================================
// �J������
// 20220729:�p������̂��ߍč\�z
//======================================================================
using UnityEngine;

namespace VR.Players
{
    // ������e�L�X�g�f�[�^��
    [CreateAssetMenu(menuName = "Scriptable/New Create PlayerData")]
    public class PlayerData : ScriptableObject
    {
        // �e�p�����[�^
        public float fHP = 1000;
        public float fAttack = 100;
        public float fSpeed = 1;
        

        // ����
        public GameObject Left_PrimaryWeapon;
        public GameObject Left_SecondaryWeapon;
        public GameObject Right_PrimaryWeapon;
        public GameObject Right_SecondaryWeapon;
    }
}
