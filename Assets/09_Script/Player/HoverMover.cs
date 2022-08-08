//============================================================
// あの移動処理
//======================================================================
// 開発履歴
// 20220729:可用性向上のため再構築
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class HoverMover 
    {
        // 必要なもの
        public float fAccelCircleSize = 0.05f;
        public float braketime = 1;
        
        public void HeadInclinationMove(CharacterController _character ,Vector3 _anchor, Vector3 _initirizepos, float _speed)
        {
            Vector3 vector = _anchor - _initirizepos;
            Vector3 direction = new Vector3();
            bool bBreak = true;

            // 限界速度補正値
            float fAnchorX = vector.x;
            float fAnchorZ = vector.z;

            // 停止範囲外に出たとき走り出す
            if ((fAnchorZ > fAccelCircleSize || -fAccelCircleSize > fAnchorZ))
            {
                direction.z = fAnchorZ / fAccelCircleSize;
                bBreak = true;
            }

            if ((fAnchorX > fAccelCircleSize || -fAccelCircleSize > fAnchorX))
            {
                direction.x = fAnchorX / fAccelCircleSize;
                bBreak = true;
            }

            if (!bBreak)
            {
                _character.Move(direction * Time.fixedDeltaTime * _speed);
            }
            else
            {
                Debug.Log("ブレーキ");
                _character.Move(direction * Time.fixedDeltaTime * (_speed / braketime));
            }
        }
    }
}
