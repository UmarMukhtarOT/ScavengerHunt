using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance = null;
    public AudioClip levelWinSound = null;
    public AudioClip levelFailSound = null;
    public AudioClip[] coinCollectSounds = null;
    public AudioClip[] bonusCollectSounds = null;
    public AudioClip buttonClipSound = null;
    public AudioClip gemCollectSound = null;
    public AudioClip[] humanKillSounds = null;
    public AudioClip selectionSound = null;
    public AudioClip hitSound = null;
    public AudioClip backGroundSound = null;
    public AudioClip achivementSound = null;
    public AudioClip ClickScav = null;
    public AudioClip AssetCollect_Scav = null;
    public AudioClip AssetFound_Scav = null;
    public AudioSource AS = null;
    private void Awake()
    {
        instance = this;
    }
    void PlaySound(AudioClip A, AudioSource AS)
    {

        if (A == null)
            return;
        if (AS == null)
            return;
        if (AS.mute)
            return;

        AS.clip = A;
        AS.loop = false;
        AS.Play();
    }
    public void PlayLevelWinSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(levelWinSound, AS);
    }

    public void PlayLevelFailSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(levelFailSound, AS);
    }
    public void PlayCoinCollectSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(coinCollectSounds[Random.Range(0, coinCollectSounds.Length)], AS);
    }
    public void PlayBonusCollectSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(bonusCollectSounds[Random.Range(0, bonusCollectSounds.Length)], AS);
    }

    public void PlayButtonClipSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(buttonClipSound, AS);
    }

    public void PlayGemCollectSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(gemCollectSound, AS);
    }
    public void PlayHumanKillSounds(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(humanKillSounds[Random.Range(0, humanKillSounds.Length)], AS);
    }

    public void PlaySelectionSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(selectionSound, AS);
    }

    public void PlayHitSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(hitSound, AS);
    }
    public void PlayBackGroundSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(backGroundSound, AS);
    }
    public void PlayAchivementSound(AudioSource AS)
    {
        if (AS == null)
            return;
        AS.volume = 1f;
        PlaySound(achivementSound, AS);
    }


    public void PlayScavSound(AudioClip Ac)
    {


        if (AS == null)
            return;


        AS.clip = Ac;
        AS.loop = false;
        AS.Play();
    }
}
