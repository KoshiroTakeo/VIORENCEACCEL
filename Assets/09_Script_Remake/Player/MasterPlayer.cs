//============================================================
// �V�[����̃v���C���[
//======================================================================
// �J������
// 20220728:�p������̂��ߍč\�z
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class MasterPlayer : MonoBehaviour
    {
        // �K�v�R���|�[�l���g�iUnity�ˑ��j
        CharacterController PlayerCharacter;
        GameObject AnchorObject;
        [SerializeField]
        GameObject CenterEyeAnchor;
        Vector3 InitirizeAnchorPos = new Vector3();

        // �K�v�N���X�i������j
        // Player�̃p�����[�^�f�[�^
        [SerializeField] PlayerData Data;
        PlayerParameter Parameter;

        // �ړ��N���X
        StickMover NormalMove;
        HoverMover HoverMove;

        bool ModeSwitch = true;

        // �U���N���X

        // �G�t�F�N�g�N���X


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

