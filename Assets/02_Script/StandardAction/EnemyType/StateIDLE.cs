using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class Idles : EnemyState
    {

        public Idles(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.IDLE;
            nowAnimation = ANIME.isIdle;
        }

        public override void Enter()
        {          
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

            if (Random.Range(0, 2000) < 10)
            {
                nextEnemyState = new Patrols(npc, agent, anim, player);
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
