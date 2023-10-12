using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] Sound[] bgm = null;
    [SerializeField] Sound[] sfx = null;

    [Space]
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Awake()
    {
        if(instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        
    }

    private void Start()
    {

        bgmPlayer.clip = bgm[0].clip;
        bgmPlayer.Play();

    }

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if(bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlayerSFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if(sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if(!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 효과음이 플레이 중");
                return;
            }
        }
        Debug.Log("같은 이름의 효과음이 없음");
        return;
    }
}
