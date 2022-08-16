//============================================================
// 加速開始円
//======================================================================
// 開発履歴
// 20220811:
// 20220816:Debug.Logを参考にインスタンス（？）化、マテリアルと移動が課題
//======================================================================
using UnityEngine;

public class DrawCircle 
{
    //
    private static GameObject CircleObj;
    // 描画用
    private static LineRenderer Render;
    // 円の頂点数
    private static int segment = 16;
    // 円の線の太さ
    private static float width = 1;
    // 設定初期位置
    private static Vector3 oldSetPos;

    // 停止中色(0816 灰色になる)
    private static Color StopColor = new Color(0f,0.8f,1f,1f);
    // 移動中色(0816 灰色になる)
    private static Color MovingColor = new Color(0f,1f,1f,1f);


    public static void SetCircle(GameObject _parent, float _radius, Vector3 _setpos, float _accel)
    {
        if (Render == null)
        {
            Debug.Log("インスタンス？");

            CircleObj = new GameObject("AccelCircle");
            CircleObj.transform.SetParent(_parent.transform);
            Render = CircleObj.AddComponent<LineRenderer>();
            Render.startWidth = width;
            Render.endWidth = width;
            Render.positionCount = segment;

            

            oldSetPos = _setpos;

            Debug.Log("再描画");
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

            Debug.Log("終了");
        }


        // 初期位置が変更されたとき
        if(!(oldSetPos == _setpos))
        {
            Debug.Log("再描画");
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
            Debug.Log("完了");
        }


        CircleObj.transform.position = _parent.transform.position;
        Render.material.SetColor("_Color", new Color(0f, _accel, 1f, 1f));

    }


}
