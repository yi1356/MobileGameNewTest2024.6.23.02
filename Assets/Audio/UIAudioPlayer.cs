using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 放一次UI的音效
/// </summary>
[CreateAssetMenu(menuName="Audio/UIAudioPlayer")]
public class UIAudioPlayer : ScriptableObject
{
    [SerializeField] AudioClip ClickAudioClip;
    [SerializeField] AudioClip CommitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;
    [SerializeField] AudioClip WinAudioClip;

    //点击按钮UI音效
    public void PlayerClick()
    {
        playerAudio(ClickAudioClip);
    }
    //提交按钮音效
    public void PlayerCommit()
    {
        playerAudio(CommitAudioClip);
    }
    //选择UI音效
    public void PlayerSelect()
    {
        playerAudio(SelectAudioClip);
    }

    //播放传入的音效
    void playerAudio(AudioClip audio)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audio);
    }
    //播放胜利音效
    internal void Playerwin()
    {
        playerAudio(WinAudioClip);
    }
}
