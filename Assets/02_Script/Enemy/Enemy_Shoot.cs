using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject MazzleFire;
    [SerializeField] SoundManager SoundManager;
    [SerializeField] EnemyManager enemyManager;
    Transform mazzle;
    public Transform mazzle1;
    public Transform mazzle2;
    float speed = 40;
    public bool horming = false;

    void Start()
    {
        
        mazzle = this.transform.Find("Muzzle");
        
        
    }

    public void BeAttack()
    {
        SoundManager = enemyManager.SoundManager;

        if (horming == false)
        {
            GameObject sparkfire = Instantiate(MazzleFire, mazzle.position, mazzle.rotation);
            SoundManager.Play_EnemyGunShot(this.gameObject);
            GameObject spawnedBullet = Instantiate(bullet, mazzle.position, mazzle.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * mazzle.forward;
            Destroy(spawnedBullet, 2);
            Destroy(sparkfire, 1);
        }
        else
        {
            SoundManager.Play_EnemyGunShot(this.gameObject);
            GameObject spawnedBullet = Instantiate(bullet, mazzle.position, mazzle.rotation);
            GameObject spawnedBullet1 = Instantiate(bullet, mazzle1.position, mazzle.rotation);
            GameObject spawnedBullet2 = Instantiate(bullet, mazzle2.position, mazzle.rotation);
            //spawnedBullet.GetComponent<Rigidbody>().velocity = speed * mazzle.forward;
            
            

        }
    }

    public void Attack_Horming()
    {

    }
}
