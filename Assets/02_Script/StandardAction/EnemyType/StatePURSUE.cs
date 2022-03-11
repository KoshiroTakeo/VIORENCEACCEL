using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class Pursues : EnemyState
    {
        float oldSpeed;

        public Pursues(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player)
            : base(_npc, _agent, _anim, _player)
        {
            name = STATE.PURSUE;
            nowAnimation = ANIME.isRunning;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            anim.SetTrigger(nowAnimation.ToString());
            oldSpeed = agent.speed;
            agent.speed *= 2;
            base.Enter();
        }

        public override void Updata()
        {
            agent.SetDestination(player.transform.position);
            if (agent.hasPath)
            {

                if (CanAttackPlayer())
                {
                    nextEnemyState = new Attacks(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
                else if (!CanSeePlayer())
                {

                    nextEnemyState = new Idles(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
            }

        }

        public override void Exit()
        {
            anim.ResetTrigger(nowAnimation.ToString());
            agent.speed = oldSpeed;
            base.Exit();
        }
    }

}