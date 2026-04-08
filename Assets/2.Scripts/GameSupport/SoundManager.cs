using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip PickSound;
    public AudioSource WalkAudio;
    public AudioSource SoundAudio;
    public AudioSource SoundAudio2;
    public AudioSource MusicAudio;

    private void Awake()
    {
        Instance = this;
    }
    

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        SoundAudio.transform.position = position;
        SoundAudio.clip = clip;
        SoundAudio.volume = 0.3f;
        SoundAudio.Play();
    }
    public void PlaySound2(AudioClip clip, Vector3 position)
    {
        SoundAudio2.transform.position = position;
        SoundAudio2.clip = clip;
        SoundAudio2.volume = 0.3f;
        SoundAudio2.Play();
    }
    //총소리는 앞의 사운드가 끝나든 말든 무조건 호출할 때마다 재생해야 한다.
    //발소리는 앞의 사운드가 끝나고 재생해야 한다...
    public void PlayMusic(AudioClip clip, Vector3 position)
    {
        if (MusicAudio.isPlaying)
        {
            StartCoroutine(MusicChange(clip, position));
        }
    }
    IEnumerator MusicChange(AudioClip clip, Vector3 position)
    {
        while (MusicAudio.isPlaying)
        {
            yield return null;
        }
        MusicAudio.clip = clip;
        MusicAudio.transform.position = position;
        MusicAudio.Play();
    }
    public void PlayWalkSound(AudioClip clip, Vector3 position)
    {
        if (!WalkAudio.isPlaying)
        {
            WalkAudio.clip = clip;
            WalkAudio.transform.position = position;
            WalkAudio.Play();
        }
    }
    public void PlayPickSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(PickSound, position);
    }


    //음악 재생(오디오, 루프 여부, 재생 위치) : 이전 음악이 있다면 정지하고 새 오디오 재생
    //음악 정지
}
