using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 音乐模块，开启、关闭、调整背景音乐；开启、关闭、调整音效
/// </summary>
public class MusicMgr : SingletonManager<MusicMgr>
{
    #region 帧检测，检测到没有播放的音效就删除
    public MusicMgr()
    {
        MonoManager.Instance.AddUpdateListener(Myupdate);
    }
    void Myupdate()
    {
        for (int i= soundList.Count-1;i>=0;i--)
        {
            if (!soundList[i].isPlaying) { StopSound(soundList[i]); }
        }
    }
    #endregion
    private AudioSource bkMusic = null;
    private float bkValue = 1f;

    private GameObject soundObj = null;
    private float sounDValue = 1f;
    private List<AudioSource> soundList = new List<AudioSource>();
    #region 背景音乐
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayerBkMusic(string name)
    {
        if (bkMusic==null)
        {
            GameObject obj = new GameObject();
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐，加载完成后 播放
        ResourcesMgr.Instance.LoadAsyn<AudioClip>("Music/BK/"+name,(clip)=>
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }
    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void StopBkMusic()
    {
        if (bkMusic==null) { return; }
        bkMusic.Stop();
    }
    /// <summary>
    ///暂停背景音乐
    /// </summary>
    public void PauseBkMusic()
    {
        if (bkMusic == null) { return; }
        bkMusic.Pause();
    }
    /// <summary>
    /// 改变背景音乐、音量大小
    /// </summary>
    /// <param name="v"></param>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic == null) { return; }
        bkMusic.volume = bkValue;
    }
    #endregion
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callBack"></param>
    public void PlayerSound(string name, bool isLoop,UnityAction<AudioSource> callBack=null)//加入委托，是为了找到这个AudioSource，并做一些操作
    {
        if (soundObj==null)
        {
            soundObj = new GameObject("soundObj");
        }
        //音效资源异步加载结束后 再添加一个音效
        ResourcesMgr.Instance.LoadAsyn<AudioClip>("Music/Sound/" + name,(clip)=>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = sounDValue;
            source.Play();
            soundList.Add(source);
            source.loop = isLoop;
            if (callBack!=null) { callBack.Invoke(source); }
        });
    }
    /// <summary>
    /// 停止传入的音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source)) 
        { source.Stop(); soundList.Remove(source);GameObject.Destroy(source); }
    }
    /// <summary>
    /// 改变音效的音量
    /// </summary>
    /// <param name="volum"></param>
    public void changeSoundValue(float volum)
    {
        sounDValue = volum;
        for (int i=0; i< soundList.Count;i++)
        {
            soundList[i].volume = sounDValue;
        }
    }

}
