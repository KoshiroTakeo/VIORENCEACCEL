using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //クエリ(「リストの中から特定の条件に合うものだけを表示する」等に使う)


//ゲームの環境構築
public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> checkpoints = new List<GameObject>();
    
    //チェックポイントのリストを取得
    public List<GameObject> Checkpoints { get { return checkpoints; } }



    //タグ「チェックポイント」に設定されてるものを入力
    public static GameEnvironment Singleton
    {
        get
        {
            //存在しないときに作成
            if (instance == null)
            {
                instance = new GameEnvironment();

                //タグの配列の中身を全て追加する
                instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                if (GameObject.FindGameObjectsWithTag("Checkpoint") == null) Debug.LogError("シーンに「CheckPoint」が存在しません");
                //リストを昇順に並び替える（List指定 => 並べ替える基準の要素を指定（ソート））
                instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList(); //ToList()でリスト化
            }
            return instance;
        }
    }

}
