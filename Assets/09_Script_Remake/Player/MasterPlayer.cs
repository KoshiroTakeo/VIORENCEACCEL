//============================================================
// シーン上のプレイヤー
//======================================================================
// 開発履歴
// 20220728:可用性向上のため再構築
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class MasterPlayer : MonoBehaviour
    {
        // 必要コンポーネント（Unity依存）
        CharacterController PlayerCharacter;
        GameObject AnchorObject;
        [SerializeField]
        GameObject CenterEyeAnchor;
        Vector3 InitirizeAnchorPos = new Vector3();

        // 必要クラス（自制作）
        // Playerのパラメータデータ
        [SerializeField] PlayerData Data;
        PlayerParameter Parameter;

        // 移動クラス
        StickMover NormalMove;
        HoverMover HoverMove;

        bool ModeSwitch = true;

        // 攻撃クラス

        // エフェクトクラス


        private void Start()
        {
            PlayerCharacter = GetComponent<CharacterController>();
            AnchorObject = new GameObject("AnchorObject");
            MoveAnchor moveAnchor = new MoveAnchor(CenterEyeAnchor, this.gameObject);
            moveAnchor = AnchorObject.AddComponent<MoveAnchor>();
            moveAnchor.Centereye = CenterEyeAnchor;
            moveAnchor.PlayerObj = this.gameObject;

            Debug.Log(moveAnchor.Centereye);


            Parameter = new PlayerParameter(Data);
            NormalMove = new StickMover();
            HoverMove = new HoverMover();
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                InitirizeAnchorPos = AnchorObject.transform.position;
            }

            HoverMove.HeadInclinationMove(PlayerCharacter, AnchorObject.transform.position, InitirizeAnchorPos,Parameter.fSpeed);
        }
    }
}

