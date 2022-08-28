//============================================================
// 加速開始円
//======================================================================
// 開発履歴
// 20220811:
// 20220816:Debug.Logを参考にインスタンス（？）化、マテリアルと移動が課題
//======================================================================
using UnityEngine;

public class DrawCircle :MonoBehaviour
{
    static DrawCircle mine;

    public  GameObject CircleObj;
    // 描画用
    private LineRenderer Render;
    // 円の頂点数
    private int segment = 16;
    // 円の線の太さ
    private float width = 0.2f;
    // 設定初期位置
    private Vector3 oldSetPos;
    // 

    // 停止中色(0816 灰色になる)
    private static Color StopColor = new Color(0f, 0.8f, 1f, 1f);
    // 移動中色(0816 灰色になる)
    private static Color MovingColor = new Color(0f, 1f, 1f, 1f);

    public void Draw(GameObject _parent, float _radius, Vector3 _setpos, float _accel)
    {
        //Debug.Log(_setpos);
        CircleObj.transform.SetParent(_parent.transform);
        Render.startWidth = width;
        Render.endWidth = width;
        Render.positionCount = segment;

        CircleObj.transform.position = _parent.transform.position;

        // 円描画
        // 課題：CenterEyeのposition（localPositionでない）を取得すること
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
        //    Debug.Log("再描画");
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
        //    Debug.Log("完了");
        //}


        
        //Render.material.SetColor("_Color", new Color(0f, _accel, 1f, 1f));
    }
}
