using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
/// <summary>
/// 用到的静态类：暂停，放场景种的音效
/// </summary>
public static  class GameplayerStatics 
{
    //unity自带的对象池
    static private ObjectPool<AudioSource> AudioPool = new ObjectPool<AudioSource>(CreateAudioSource,null,null,DestoryAudioSource,false,5,20);

    //对象池用到的，销毁物体方法
    private static void DestoryAudioSource(AudioSource obj)
    {
        GameObject.Destroy(obj);
    }
    //对象池用到的，创建物体的方法
    private static AudioSource CreateAudioSource()
    {
       GameObject  audio = new GameObject("Audio",typeof(AudioSource));
       AudioSource audioSource = audio.GetComponent<AudioSource>();
       return audioSource;
    }

    /// <summary>
    /// 外部调用的，播放音效的方法(包括回收)
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    public static  void PlayOnLoc(AudioClip clip,Vector3 pos)
    {
        AudioSource audio=AudioPool.Get();
        audio.transform.position = pos;
        audio.PlayOneShot(clip,1f);
        MonoManager.Instance.StartCoroutine(IRelease(clip, audio));
        
    }
    /// <summary>
    /// 这个协程作用：在音效片段播放完毕后，使用对象池回收回收AudioSource
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="audio"></param>
    /// <returns></returns>
    static IEnumerator IRelease(AudioClip clip, AudioSource audio)
    {
        float a = clip.length;
        yield return new WaitForSeconds(a);
        AudioPool.Release(audio);
    }

    public static void SetGamePause(bool isPause)
    {
        Time.timeScale = isPause ? 0 : 1;
    }
}
