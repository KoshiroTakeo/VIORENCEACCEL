//============================================================
// �����J�n�~
//======================================================================
// �J������
// 20220811:
// 20220816:Debug.Log���Q�l�ɃC���X�^���X�i�H�j���A�}�e���A���ƈړ����ۑ�
//======================================================================
using UnityEngine;

public class DrawCircle :MonoBehaviour
{
    static DrawCircle mine;

    public  GameObject CircleObj;
    // �`��p
    private LineRenderer Render;
    // �~�̒��_��
    private int segment = 16;
    // �~�̐��̑���
    private float width = 0.2f;
    // �ݒ菉���ʒu
    private Vector3 oldSetPos;
    // 

    // ��~���F(0816 �D�F�ɂȂ�)
    private static Color StopColor = new Color(0f, 0.8f, 1f, 1f);
    // �ړ����F(0816 �D�F�ɂȂ�)
    private static Color MovingColor = new Color(0f, 1f, 1f, 1f);

    public void Draw(GameObject _parent, float _radius, Vector3 _setpos, float _accel)
    {
        //Debug.Log(_setpos);
        CircleObj.transform.SetParent(_parent.transform);
        Render.startWidth = width;
        Render.endWidth = width;
        Render.positionCount = segment;

        CircleObj.transform.position = _parent.transform.position;

        // �~�`��
        // �ۑ�FCenterEye��position�ilocalPosition�łȂ��j���擾���邱��
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
    }

    private void Awake()
    {
        //if (!(mine == null)) return;
        CircleObj = new GameObject("AccelCircleP");
        Render = CircleObj.AddComponent<LineRenderer>();
    }

    private void Update()
    {
        //if (!(oldSetPos == _setpos))
        //{
        //    Debug.Log("�ĕ`��");
        //    var points = new Vector3[segment];
        //    for (int i = 0; i < segment; i++)
        //    {
        //        var rad = Mathf.Deg2Rad * (i * 380f / segment);
        //        var x = _setpos.x + Mathf.Sin(rad) * _radius;
        //        var z = _setpos.z + Mathf.Cos(rad) * _radius;
        //        points[i] = new Vector3(x, 5, z);
        //    }
        //    Render.SetPositions(points);

        //    oldSetPos = _setpos;
        //    Debug.Log("����");
        //}


        
        //Render.material.SetColor("_Color", new Color(0f, _accel, 1f, 1f));
    }
}
