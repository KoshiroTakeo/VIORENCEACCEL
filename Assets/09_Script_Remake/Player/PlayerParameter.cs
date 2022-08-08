//============================================================
// プレイヤーの能力値
//======================================================================
// 開発履歴
// 20220729:可用性向上のため再構築
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class PlayerParameter
    {
        // 各パラメータ
        public float fHP = 1000;
        float fMaxHP;
        public float fAttack = 100;
        public float fSpeed = 1;


        // 装備
        public GameObject Left_PrimaryWeapon;
        public GameObject Left_SecondaryWeapon;
        public GameObject Right_PrimaryWeapon;
        public GameObject Right_SecondaryWeapon;

        public PlayerParameter(PlayerData _data)
        {
            fMaxHP = fHP = _data.fHP;
            fAttack = _data.fAttack;
            fSpeed = _data.fSpeed;

        }

        public float CurrentHPValue()
        {
            float value;
            value = fHP / fMaxHP;

            return value;
        }
    }
}
