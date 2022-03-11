using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandardAction
{
    public class Bullet : MonoBehaviour
    {
        Character character;

        private int bulletAtk;
        private string bulletHolder;




        public void BulletStatus(int atk, string tagName)
        {
            bulletAtk = atk;
            bulletHolder = tagName;
        }

        private void OnCollisionEnter(Collision collision)
        {
            
            if(collision.gameObject.tag == "Enemy")
            {
                character = collision.gameObject.GetComponent<Character>();
                character.IsDamage(bulletAtk);

                Destroy(this.gameObject);
            }
            else 
            {
                Destroy(this.gameObject);
            }
        }
    }
}
