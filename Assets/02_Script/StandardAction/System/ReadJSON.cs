using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace StandardAction
{
    public class ReadJSON : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerData
        {
            public int hp;
            public int attack;
            public int defense;
            public float speed;
            public string name;
        }

        private void Start()
        {
            PlayerData playerData = new PlayerData();
            playerData.hp = 10;
            playerData.attack = 12;
            playerData.defense = 20;
            playerData.speed = 1.5f;
            playerData.name = "PLAYER";

            // 文字を読み取り
            string jsonstr = JsonUtility.ToJson(playerData);
            Debug.Log(jsonstr);

            // 格納
            PlayerData playerData2 = JsonUtility.FromJson<PlayerData>(jsonstr);
            Debug.Log(playerData2.hp);

            // セーブ
            savePlayerData(playerData2);

            // ロード
            loadPlayerData();
        }

        public void savePlayerData(PlayerData player)
        {

            StreamWriter writer;

            string jsonstr = JsonUtility.ToJson(player);

            writer = new StreamWriter(Application.dataPath + "/savedatas.json", false);

            // 書き込み
            writer.Write(jsonstr);

            //
            writer.Flush();

            //
            writer.Close();
            
        }

        public PlayerData loadPlayerData()
        {
            string datastr = "";
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/savedatas.json");
            datastr = reader.ReadToEnd();
            reader.Close();

            Debug.Log(datastr);

            return JsonUtility.FromJson<PlayerData>(datastr);
        }


    }
}
