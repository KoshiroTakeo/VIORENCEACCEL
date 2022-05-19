//============================================================
// Controller.cs
//======================================================================
// 開発履歴
//
// 2022/05/01 author 竹尾：制作、MoveManagerに必要
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnchor : MonoBehaviour
{
    public GameObject centereye; // カメラ座標取得
    public GameObject PlayerObj; // プレイヤー自体


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0,3,0);
        this.transform.rotation = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(centereye.transform.localPosition.x,30, centereye.transform.localPosition.z);
        this.transform.rotation = PlayerObj.transform.rotation;
    }
}
