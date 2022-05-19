//======================================================================
// BGMPlayer.cs
//======================================================================
// �J������
//
// 2022/04/21 author:�|���W�j�Y�@����
//                               BGM�̂݃C���g�������[�v�ł���悤�ɂ���
//                               �q�����Â�(�����̖��H)
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

    // BGM��~ *************************************************
    public void StopBGM()
    {
        Intro.clip = EnvSound_L.clip;
        Intro.Play();
        Loop.Stop();
        //EnvSound_L.Stop();
        //EnvSound_R.Stop();
    }
    //**********************************************************

    

    // BGM�ĊJ *************************************************
    public void ReStartBGM()
    {
        Intro.Play();
        Loop.Play();
        //EnvSound_L.Play();
        //EnvSound_R.Play();
    }
    //**********************************************************

    // �Đ�  ***************************************************
    public void Play_BGM()
    {
        int num = 0;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num]);
    }

    //**********************************************************



    // BGN�Đ� *************************************************
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

    
    // ���I������ƃR���|�[�l���g�폜
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
