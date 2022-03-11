using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandardAction
{
    public class EnemyLoadStatus : MonoBehaviour
    {
        public class EnemyStutasData
        {
            public string type = "近接";// 各タイプにモデル一つ
            public int level = 1;
            public int hp = 1;
            public float atk = 1;
            public float def = 1;
            public float speed = 1;
            public float jumpPower = 1;
            public float bulletSpeed = 1;
        }
        [SerializeField] List<EnemyStutasData> enemyList = new List<EnemyStutasData>();

        public string stagePath;
        //JsonNode json;

        private void Start()
        {
            EnemyStutasData enemyStutasData = new EnemyStutasData();
            EnemyStutasData setStutasData = new EnemyStutasData();
            int index = 0;
            string jsonText = Resources.Load<TextAsset>("JsonData\\" + stagePath + "\\EnemyList").ToString();
            //json = JsonNode.Parse(jsonText);

            //foreach(JsonNode note in json["LIST"])
            //{
            //    enemyStutasData.type = note["type"].Get<string>();
            //    enemyStutasData.level = int.Parse(note["level"].Get<string>());
            //    enemyStutasData.hp = int.Parse(note["hp"].Get<string>());
            //    enemyStutasData.atk = float.Parse(note["atk"].Get<string>());
            //    enemyStutasData.def = float.Parse(note["def"].Get<string>());
            //    enemyStutasData.speed = float.Parse(note["speed"].Get<string>());
            //    enemyStutasData.jumpPower = float.Parse(note["jumpPower"].Get<string>());
            //    enemyStutasData.bulletSpeed = float.Parse(note["bulletSpeed"].Get<string>());

            //    enemyList[index] = enemyStutasData;

            //    enemyStutasData = new EnemyStutasData();
            //    index++;
            //}

        }
    }

}
