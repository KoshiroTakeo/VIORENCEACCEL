//============================================================
// ���̈ړ�����
//======================================================================
// �J������
// 20220729:�p������̂��ߍč\�z
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class HoverMover 
    {
        // �K�v�Ȃ���
        public float fAccelCircleSize = 0.05f;
        public float braketime = 1;
        
        public void HeadInclinationMove(CharacterController _character ,Vector3 _anchor, Vector3 _initirizepos, float _speed)
        {
            Vector3 vector = _anchor - _initirizepos;
            Vector3 direction = new Vector3();
            bool bBreak = true;

            // ���E���x�␳�l
            float fAnchorX = vector.x;
            float fAnchorZ = vector.z;

            // ��~�͈͊O�ɏo���Ƃ�����o��
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
                Debug.Log("�u���[�L");
                _character.Move(direction * Time.fixedDeltaTime * (_speed / braketime));
            }
        }
    }
}
