//======================================================================
// SoundManager.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author:竹尾晃史郎　制作
//                               SEあまりにも再生しすぎると重くなる
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 音データリスト
    public SoundData SoundData;

    private void Update()
    {
        
    }

    // PlayerのSE ***********************************************
    public void Play_PlayerDamage(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[0]);
    }

    public void Play_PlayerGunShot(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[1]);
    }
    //**********************************************************

    // EnemyのSE ***********************************************
    public void Play_EnemyDamage(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[0]);
    }

    public void Play_EnemyGunShot(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[1]);
    }
    //**********************************************************

    // SystemのSE **********************************************
    public void Play_SystemDecide(GameObject system)
    {
        PlaySE(system, SoundData.SystemSoundList[0]);
    }
    //**********************************************************




    // SE再生
    void PlaySE(GameObject obj, AudioClip clip)
    {
        AudioSource audioSource;
        audioSource = obj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        StartCoroutine(Checking(audioSource));
    }

    // 音終了判定とコンポーネント削除
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
