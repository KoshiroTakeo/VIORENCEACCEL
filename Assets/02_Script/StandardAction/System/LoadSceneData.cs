using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace StandardAction
{

    public class LoadSceneData : MonoBehaviour
    {


        void LoadEnemyData()
        {

        }

        void SaveEnemyData()
        {
            string datastr = "";
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/savedatas.json");
            datastr = reader.ReadToEnd();
            reader.Close();

            

            
        }
    }
}
