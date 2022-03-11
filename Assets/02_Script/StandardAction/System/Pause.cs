using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandardAction
{
    public class Pause : MonoBehaviour
    {
        GameObject pauseUIPrefab;
        GameObject pauseUIInstance;
        GameManager gameManager;

        public void PauseMenu(bool pause)
        {
            if(pause)
            {
                if(pauseUIInstance == null)
                {
                    pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                    Time.timeScale = 0;
                }
                else
                {
                    Destroy(pauseUIInstance);
                    Time.timeScale = 1;
                }
            }
        }

        

    }
}

