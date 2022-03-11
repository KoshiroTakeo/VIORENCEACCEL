using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class EnemyState : MonoBehaviour
    {
        public enum STATE
        {
            IDLE, WANDER, PATROL, PURSUE, ATTACK, RUNAWAY, FUNNEL
        }
        
        public enum EVENT
        {
            ENTER, UPDATA, EXIT
        }

        public enum ANIME
        {
            isIdle, isWalking,isRunning,isAttack
        }
        

        public STATE name; //現在の行動
        public ANIME nowAnimation; //動かしているアニメ
        protected EVENT stage;              //
        protected GameObject npc;           //
        protected GameObject player;         //プレイヤー座標
        protected Animator anim;
        protected NavMeshAgent agent;       //ナビメッシュ
        protected EnemyState nextEnemyState;//次の行動
        protected Enemy enemySC;

        float recognizeDist = 5;
        float recognizeAngle = 120;
        float attackDist = 3;

        // 次の行動に移行させる
        public virtual void Enter() { stage = EVENT.UPDATA; enemySC.currentState = name.ToString(); }
        public virtual void Updata() { stage = EVENT.UPDATA; }  
        public virtual void Exit() { stage = EVENT.EXIT; }

        // このスクリプトに情報を入れる為のもの(行数省略のため)
        protected EnemyState(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player)
        {
            npc = _npc;       
            agent = _agent;      
            anim = _anim;               
            player = _player;     
            stage = EVENT.ENTER;

            enemySC = npc.GetComponent<Enemy>();
            
            
        }

        // 行動遷移用
        public EnemyState Process()
        {
            //stateから読み取る
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATA) Updata(); 
            if (stage == EVENT.EXIT)             
            {
                Exit();
                return nextEnemyState; 
            }

            return this; 
        }


        //プレイヤー感知(true/falseで返す)
        public bool CanSeePlayer()
        {
            Vector3 direction = player.transform.position - npc.transform.position;  
            float angle = Vector3.Angle(direction, npc.transform.forward); 

            if (direction.magnitude < recognizeDist && angle < recognizeAngle)        
            {
                return true;
            }
            return false;
        }

       
        //プレイヤーへ攻撃する(true/falseで返す)
        public bool CanAttackPlayer()
        {
            Vector3 direction = player.transform.position - npc.transform.position;  
            if (direction.magnitude < attackDist)                          
            {
                return true;
            }
            return false;
        }
        

        
    }

}

