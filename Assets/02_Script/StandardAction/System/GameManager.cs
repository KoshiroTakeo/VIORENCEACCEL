using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandardAction
{
    public class GameManager : MonoBehaviour
    {
        SceneTransitionManager sceneTransitionManager;
        EnemyGeneratar enemyGeneratar;
        
        

        private void Awake()
        {
            sceneTransitionManager = new SceneTransitionManager();
            enemyGeneratar = new EnemyGeneratar();
        }

        private void Start()
        {
           
        }

        private void Update()
        {
            
        }

        

        public void GameOverPlayer(int hp)
        {
            if(hp <= 0)
            {
                sceneTransitionManager.SceneTransition("GAMEOVER");
            }
            
        }

        
    }

}
