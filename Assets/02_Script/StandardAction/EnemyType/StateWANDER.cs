using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class Wander : EnemyState
    {
        public Wander(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.WANDER;
            nowAnimation = ANIME.isWalking;
        }

        public override void Enter()
        {
            Vector3 pos = new Vector3(Randomfloat(), 0, Randomfloat());
            agent.SetDestination(pos);
            anim.SetTrigger(nowAnimation.ToString());
            base.Enter();
        }

        public override void Updata()
        {
            if (CanSeePlayer())
            {
                nextEnemyState = new Pursues(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

            if (Random.Range(0, 2000) < 10 || agent.remainingDistance < 1)
            {
                nextEnemyState = new Idles(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        
        public override void Exit()
        {
            anim.ResetTrigger(nowAnimation.ToString());
            base.Exit();
        }

        float Randomfloat()
        {
            float rand = Random.Range(-enemySC.targetPos, enemySC.targetPos);
            return rand;
        }
    }

}
