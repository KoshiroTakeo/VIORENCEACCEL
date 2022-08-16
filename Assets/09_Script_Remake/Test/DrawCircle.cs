//============================================================
// �����J�n�~
//======================================================================
// �J������
// 20220811:
// 20220816:Debug.Log���Q�l�ɃC���X�^���X�i�H�j���A�}�e���A���ƈړ����ۑ�
//======================================================================
using UnityEngine;

public class DrawCircle 
{
    //
    private static GameObject CircleObj;
    // �`��p
    private static LineRenderer Render;
    // �~�̒��_��
    private static int segment = 16;
    // �~�̐��̑���
    private static float width = 1;
    // �ݒ菉���ʒu
    private static Vector3 oldSetPos;

    // ��~���F(0816 �D�F�ɂȂ�)
    private static Color StopColor = new Color(0f,0.8f,1f,1f);
    // �ړ����F(0816 �D�F�ɂȂ�)
    private static Color MovingColor = new Color(0f,1f,1f,1f);


    public static void SetCircle(GameObject _parent, float _radius, Vector3 _setpos, float _accel)
    {
        if (Render == null)
        {
            Debug.Log("�C���X�^���X�H");

            CircleObj = new GameObject("AccelCircle");
            CircleObj.transform.SetParent(_parent.transform);
            Render = CircleObj.AddComponent<LineRenderer>();
            Render.startWidth = width;
            Render.endWidth = width;
            Render.positionCount = segment;

            

            oldSetPos = _setpos;

            Debug.Log("�ĕ`��");
            var points = new Vector3[segment];
            for (int i = 0; i < segment; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 380f / segment);
                var x = _setpos.x + Mathf.Sin(rad) * _radius;
                var z = _setpos.z + Mathf.Cos(rad) * _radius;
                points[i] = new Vector3(x, 5, z);
            }
            Render.SetPositions(points);

            oldSetPos = _setpos;

            Debug.Log("�I��");
        }


        // �����ʒu���ύX���ꂽ�Ƃ�
        if(!(oldSetPos == _setpos))
        {
            Debug.Log("�ĕ`��");
            var points = new Vector3[segment];
            for (int i = 0; i < segment; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 380f / segment);
                var x = _setpos.x + Mathf.Sin(rad) * _radius;
                var z = _setpos.z + Mathf.Cos(rad) * _radius;
                points[i] = new Vector3(x, 5, z);
            }
            Render.SetPositions(points);

            oldSetPos = _setpos;
            Debug.Log("����");
        }


        CircleObj.transform.position = _parent.transform.position;
        Render.material.SetColor("_Color", new Color(0f, _accel, 1f, 1f));

    }


}
