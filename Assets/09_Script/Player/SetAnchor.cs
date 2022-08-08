//============================================================
// �A���J�[
//======================================================================
// �J������
// 20220729:�p������̂��ߍč\�z
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class SetAnchor : MonoBehaviour
    {
        GameObject Centereye; // �J�������W�擾
        GameObject PlayerObj; // �v���C���[����

        public GameObject AnchorObject;

        public SetAnchor(GameObject _centereye, GameObject _player)
        {
            GameObject obj = new GameObject("AnchorObject");

            Centereye = _centereye;
            PlayerObj = _player;

            obj.transform.position = new Vector3(0, 3, 0);
            obj.transform.rotation = new Quaternion();

            AnchorObject = obj;
        }

        void Update()
        {
            this.transform.position = new Vector3(Centereye.transform.localPosition.x, 30, Centereye.transform.localPosition.z);
            this.transform.rotation = PlayerObj.transform.rotation;
        }
    }
}

