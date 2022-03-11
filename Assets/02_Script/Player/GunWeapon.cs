using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    GameObject bullet;
    Transform mazzle;
    float speed = 40;
    
    //AudioSource audioSource;
    //AudioClip audioClip;

    private void Start()
    {
        bullet = (GameObject)Resources.Load("Prefab/Bullet");
        mazzle = this.transform.Find("Muzzle");

    }

    public void Fire()
    {        
        GameObject spawnedBullet = Instantiate(bullet, mazzle.position, mazzle.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * mazzle.forward;
        //audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }

    
}
