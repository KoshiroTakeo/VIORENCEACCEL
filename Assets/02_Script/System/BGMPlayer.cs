//======================================================================
// BGMPlayer.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author:竹尾晃史郎　制作
//                               BGMのみイントロ→ループできるようにする
//                               繋ぎが甘い(音源の問題？)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public SoundData SoundData;

    public AudioSource Intro;
    public AudioSource Loop;
    public AudioSource EnvSound_L;
    public AudioSource EnvSound_R;


    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        Play_BGM();
    }

    private void Update()
    {
        
    }

    // BGM停止 *************************************************
    public void StopBGM()
    {
        Intro.clip = EnvSound_L.clip;
        Intro.Play();
        Loop.Stop();
        //EnvSound_L.Stop();
        //EnvSound_R.Stop();
    }
    //**********************************************************

    

    // BGM再開 *************************************************
    public void ReStartBGM()
    {
        Intro.Play();
        Loop.Play();
        //EnvSound_L.Play();
        //EnvSound_R.Play();
    }
    //**********************************************************

    // 再生  ***************************************************
    public void Play_BGM()
    {
        int num = 0;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num]);
    }

    //**********************************************************



    // BGN再生 *************************************************
    void PlayEnvSound(AudioClip clip)
    {
        EnvSound_L.clip = EnvSound_R.clip = null;
        EnvSound_L.clip = EnvSound_R.clip = clip;
        EnvSound_L.loop = EnvSound_R.loop = true;
        EnvSound_L.Play(); EnvSound_R.Play();
    }

    void StartIntro(AudioClip clip, int listnum)
    {
        Intro.clip = null;
        Loop.clip = null;
        Intro.clip = clip;
        Intro.Play();
        StartCoroutine(Checking(Intro, listnum));
    }

    void ChangeLoopBGM(int Intronum)
    {
        Loop.clip = SoundData.StageBGMSoundList[Intronum + 1];
        Loop.Play();
        Loop.loop = true;
    }
    //**********************************************************

    
    // 音終了判定とコンポーネント削除
    private IEnumerator Checking(AudioSource audio, int num)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                ChangeLoopBGM(num);
                break;
            }
        }
    }

}
