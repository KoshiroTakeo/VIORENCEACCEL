using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject MazzleFire;
    [SerializeField] SoundManager SoundManager;
    [SerializeField] OVRInput.Controller Controller;
    Transform mazzle;
    float speed = 40;
    
    //AudioSource audioSource;
    //AudioClip audioClip;

    private void Start()
    {
        mazzle = this.transform.Find("Muzzle");

    }

    public void Fire()
    {
        StartCoroutine(VibrateForSeconds(0.2f, 0.2f, 0.2f, Controller));
        Instantiate(MazzleFire, mazzle.position, mazzle.rotation);
        SoundManager.Play_PlayerGunShot(this.gameObject);
        GameObject spawnedBullet = Instantiate(bullet, mazzle.position, mazzle.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * mazzle.forward;
        Destroy(spawnedBullet, 2);
    }


    IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        // U“®ŠJn
        OVRInput.SetControllerVibration(frequency, amplitude, controller); //(U“®”AU•A‰E‚©¶‚©)

        // U“®ŠÔŠu•ª‘Ò‚Â
        yield return new WaitForSeconds(duration);

        // U“®I—¹
        OVRInput.SetControllerVibration(0, 0, controller);
    }

}
