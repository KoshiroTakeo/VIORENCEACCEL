//============================================================
// 操作系
//======================================================================
// 開発履歴
// 20220729:可用性向上のため再構築
//======================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using VR.Players;

namespace VR
{
    public class InputManager : MonoBehaviour
    {
        //入力を取得
        PlayerInput InputData;
        InputAction _moveAction, _LGuardAction, _RGuardAction, _menuAction;
        public Vector3 AnchorPos;

        // 入力を受け付けるクラス達
        MasterPlayer Player;
        // UI(メニュー)

        private void Start()
        {
            InitController(InputData = GetComponent<PlayerInput>());
            Player = GameObject.FindWithTag("Player").GetComponent<MasterPlayer>();
        }

        // 操作設定 =================================
        void InitController(PlayerInput _input)
        {
            var actionMap = _input.currentActionMap;

            //アクションマップからアクションを取得
            _moveAction = actionMap["Move"];
            
        }
        //===========================================
    }
}

