using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class Patrols : EnemyState
    {
        int currentIndex = -1;
        public Patrols(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.PATROL;
            nowAnimation = ANIME.isWalking;
        }

        public override void Enter()
        {
            
            float lastDist = Mathf.Infinity;                                                          //最初は距離を無限とする(すべてのチェックポイントを探知するため)
            for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)                     //チェックポイントの数だけ周る
            {
                GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];                         //目的地を読み込む
                
                float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position); //目的地と自身の距離
                if (distance < lastDist)                                                              //たどり着いてなければ行う
                {
                    currentIndex = i - 1;
                    lastDist = distance;                                                              //無限の距離を次の目的地までとする
                }
            }
            
            anim.SetTrigger(nowAnimation.ToString());
            base.Enter();
        }

        public override void Updata()
        {
            //Debug.Log(agent.name);
            //Debug.Log(agent.remainingDistance);
            

            if (agent.remainingDistance < 1)                                                                   //エージェントの位置および現在の経路での目標地点の間の距離
            {
               
                if (currentIndex >= EnemyWayPoint.Singleton.Checkpoints.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;
                }
                //Debug.Log(currentIndex);
                agent.SetDestination(EnemyWayPoint.Singleton.Checkpoints[currentIndex].transform.position);
            }

            if (CanSeePlayer())
            {
                nextEnemyState = new Pursues(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger(nowAnimation.ToString());
            base.Exit();
        }
    }
}