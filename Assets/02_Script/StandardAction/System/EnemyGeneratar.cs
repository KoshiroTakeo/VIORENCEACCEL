using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace StandardAction
{
    public class EnemyGeneratar : MonoBehaviour
    {
        [System.Serializable]
        public class EnemyData
        {
            public string type = "A"; // 行動の変わる敵の種類
            public int level = 1; // 敵の強さ　(速度、弾威力、耐久)
            public int hp = 1;
            public float speed = 1.0f;  
        }
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] string FilePath;

        [SerializeField] List<EnemyData> enemyList = new List<EnemyData>();
        //private JsonNode json;

        int time;
        int interval;
        int index = 0;
        bool loadtext = false;
        
        
        


        private void Start()
        {
            
            time = 0;
            interval = 300;

        }

        private void Update()
        {
            if(time > interval)
            {
                EnemyGenarate();
                

                interval--;
                time = 0;
            }


            time++;
        }


        
        // Jsonセーブ
        void SaveEnemy()
        {
            StreamWriter writer;
            writer = new StreamWriter(Application.dataPath + "/EnemyData.json", false);

            EnemyData enemyData = new EnemyData();
            enemyData.type = "雑魚";
            enemyData.level = 1;

            string jsonstr = JsonUtility.ToJson(enemyData);

            writer.Write(jsonstr);
            writer.Flush();
            writer.Close();
        }


        void EnemyGenarate()
        {
            
            //EnemyData enemyData = new EnemyData();
            //EnemyData setEnemy = new EnemyData();
            //string jsonText = Resources.Load<TextAsset>("JsonData\\" + FilePath).ToString();
            
            //json = JsonNode.Parse(jsonText);
            
            
           
            //if(loadtext == false)
            //{
            //    foreach (JsonNode note in json["LIST"])
            //    {
            //        enemyData.type = note["type"].Get<string>();
            //        enemyData.level = int.Parse(note["level"].Get<string>());
            //        enemyData.hp = int.Parse(note["hp"].Get<string>());
            //        enemyData.speed = float.Parse(note["speed"].Get<string>());
            //        enemyList[index] = enemyData;
            //        enemyData = new EnemyData();
            //        index++;

            //    }

            //    loadtext = true;
            //}
           
            

            //int randIndex = Random.Range(0, index);
         
            //setEnemy = enemyList[randIndex];
            
            //Vector3 randPos = new Vector3(Random.Range(-20, 20), 0.5f, 35.0f);
            //GameObject enemy = Instantiate(enemyPrefab, randPos, enemyPrefab.transform.rotation);
            //enemy.GetComponent<STG_Enemy>().SetEnemyStatus(setEnemy.type, setEnemy.level, setEnemy.hp, setEnemy.speed);

            
        }
    }
}

