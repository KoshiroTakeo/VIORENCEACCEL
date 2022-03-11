using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StandardAction
{
    public class Attacks : EnemyState
    {

        float rotationSpeed = 2.0f;
        

        public Attacks(GameObject _npc, NavMeshAgent _agent, Animator _anim, GameObject _player)
            : base(_npc, _agent, _anim, _player)
        {
            name = STATE.ATTACK;
            nowAnimation = ANIME.isAttack;
        }

        public override void Enter()
        {
            anim.SetTrigger(nowAnimation.ToString());
            agent.isStopped = true;
            base.Enter();
        }

        public override void Updata()
        {
            LockatPlayer();

            if (!CanAttackPlayer())
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

        void LockatPlayer()
        {
            Vector3 direction = player.transform.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y = 0;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }
}