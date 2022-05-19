//======================================================================
// SoundManager.cs
//======================================================================
// �J������
//
// 2022/04/21 author:�|���W�j�Y�@����
//                               SE���܂�ɂ��Đ���������Əd���Ȃ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ���f�[�^���X�g
    public SoundData SoundData;

    private void Update()
    {
        
    }

    // Player��SE ***********************************************
    public void Play_PlayerDamage(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[0]);
    }

    public void Play_PlayerGunShot(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[1]);
    }
    //**********************************************************

    // Enemy��SE ***********************************************
    public void Play_EnemyDamage(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[0]);
    }

    public void Play_EnemyGunShot(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[1]);
    }
    //**********************************************************

    // System��SE **********************************************
    public void Play_SystemDecide(GameObject system)
    {
        PlaySE(system, SoundData.SystemSoundList[0]);
    }
    //**********************************************************




    // SE�Đ�
    void PlaySE(GameObject obj, AudioClip clip)
    {
        AudioSource audioSource;
        audioSource = obj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        StartCoroutine(Checking(audioSource));
    }

    // ���I������ƃR���|�[�l���g�폜
    private IEnumerator Checking(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                
                Destroy(audio);
                break;
            }
        }
    }
}
