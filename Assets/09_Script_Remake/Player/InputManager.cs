//============================================================
// ����n
//======================================================================
// �J������
// 20220729:�p������̂��ߍč\�z
//======================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using VR.Players;

namespace VR
{
    public class InputManager : MonoBehaviour
    {
        //���͂��擾
        PlayerInput InputData;
        InputAction _moveAction, _LGuardAction, _RGuardAction, _menuAction;
        public Vector3 AnchorPos;

        // ���͂��󂯕t����N���X�B
        MasterPlayer Player;
        // UI(���j���[)

        private void Start()
        {
            InitController(InputData = GetComponent<PlayerInput>());
            Player = GameObject.FindWithTag("Player").GetComponent<MasterPlayer>();
        }

        // ����ݒ� =================================
        void InitController(PlayerInput _input)
        {
            var actionMap = _input.currentActionMap;

            //�A�N�V�����}�b�v����A�N�V�������擾
            _moveAction = actionMap["Move"];
            
        }
        //===========================================
    }
}

