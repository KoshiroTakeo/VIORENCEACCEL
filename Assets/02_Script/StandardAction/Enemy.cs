using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    // Enemy特有の事をさせる
    public class Enemy : Character
    {
        // 敵の種類 *****************************
        enum  ENEMYTYPE
        {
            近接型,
            遠距離型
            
        }              
        [Header("敵の種類")]
        [SerializeField] ENEMYTYPE enemyType;
        // **************************************

        GameObject player;
        EnemyState enemyState;
        public string currentState;
        public float targetPos;

        private void Awake()
        {
            if (gameObject.GetComponent<NavMeshAgent>() == null)
            {
                gameObject.AddComponent<NavMeshAgent>();    
            }

            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
        }

        void Start()
        {
            player = GameObject.FindWithTag("Player");
           
            StrengthoftheEnemy(enemyType);
        }



        void Update()
        {
            enemyState = enemyState.Process();
            
            if(transform.position.z < -20)
            {
                hp = -1;
            }

            IsDestroy(hp);

            
        }



        void StrengthoftheEnemy(ENEMYTYPE enemyType)
        {        
            switch(enemyType)
            {
                case ENEMYTYPE.近接型:

                    hp = 200;
                    atk = 50;
                    def = 50;
                    speed = 20;
                    jumpPower = 0;
                    bulletSpeed = 20;

                    navMeshAgent.speed = speed * 200;
                    navMeshAgent.isStopped = false;
                   
                    animator = this.gameObject.GetComponent<Animator>();
                    enemyState = new Idles(this.gameObject, navMeshAgent, animator, player);

                    break;

                default:
                    Debug.LogError("設定されてない敵です");
                    break;
            }
        }

        
        public void SetEnemyStatus(int level)
        {
            characterLevel = level;
        }

        
    }
}
