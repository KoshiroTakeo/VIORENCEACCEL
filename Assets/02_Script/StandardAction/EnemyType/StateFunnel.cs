using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace StandardAction
{
    public class Funnel : EnemyState
    {
        Vector3 playerPos;
        float oldSpeed, oldAccel, oldAngle;
        int time;
        int limit;

        public Funnel(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player): base(_npc, _agent, _anim, _player)
        {
            name = STATE.FUNNEL;
            nowAnimation = ANIME.isRunning;
            agent.isStopped = false;

            time = 0;
            limit = 180;

            playerPos = player.transform.position;
            oldSpeed = agent.speed;
            oldAccel = agent.acceleration;
            oldAngle = agent.angularSpeed;
            agent.speed *= 5;
            agent.acceleration *= 10;
            agent.angularSpeed *= 10;
            agent.SetDestination(playerPos + Vector3_Sign());
        }

        public override void Enter()
        {
            //anim.SetTrigger("isRunning");
            base.Enter();
        }

        public override void Updata()
        {
            if (CanAttackPlayer())
            {
                ResetStatus();
                nextEnemyState = new Pursues(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (agent.remainingDistance < 1)
            {
                if(time < limit)
                {
                    ResetStatus();
                    nextEnemyState = new Funnel(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
                time++;
            }

            
        }

        public override void Exit()
        {
            //anim.ResetTrigger("isRunning");
            Debug.Log("aaa");
            
            base.Exit();
        }

        Vector3 Vector3_Sign()
        {
            float sin = Mathf.Sin(Time.time) * 3;
            Vector3 vector = new Vector3(sin,0,sin);

            return vector;
        }

        private void ResetStatus()
        {
            agent.speed = oldSpeed;
            agent.acceleration = oldAccel;
            agent.angularSpeed = oldAngle;
        }
    }

}