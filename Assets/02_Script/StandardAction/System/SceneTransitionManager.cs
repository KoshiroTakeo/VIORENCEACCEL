using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StandardAction
{
    //ロード、ムービーなどシーン遷移時に関する処理を行う
    public class SceneTransitionManager : MonoBehaviour
    {
        public enum SCENENAME
        {
            GAMEOVER,
            GAMECLEAR,
            LOADSCENE
        }

        //値を入れたらそのシーンに行くようにしたい
        public void SceneTransition(string sceneName)
        {
            // 演出を挟む


            switch(sceneName)
            {
                case "GAMEOVER":
                    break;

                default:
                    Debug.LogError("存在しないシーンを呼び出しました");
                    break;
            }

            SceneManager.LoadScene(sceneName);
        }

    }

}


