using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource bgmaudio;
    [SerializeField] AudioSource eventAudio;

    [SerializeField] AudioClip[] clips;

    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider eventVolume;

    [SerializeField] Slider bgmVolume2;
    [SerializeField] Slider eventVolume2;


    public bool isGameEnd = false;
    private void Awake() //초기화 작업 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bgmVolume.value = bgmaudio.volume; //현재 오디오 볼륨을 슬라이더값으로
        eventVolume.value = eventAudio.volume;

        bgmVolume2.value = bgmaudio.volume; //현재 오디오 볼륨을 슬라이더값으로
        eventVolume2.value = eventAudio.volume;

    }

    private void Start()
    {       
        BgmSoundPlay("intro");
        bgmVolume.onValueChanged.AddListener(OnBgmVolumeChange);
        eventVolume.onValueChanged.AddListener(OnEventVolumeChange);

        bgmVolume2.onValueChanged.AddListener(OnBgmVolumeChange);
        eventVolume2.onValueChanged.AddListener(OnEventVolumeChange);
    }

    public void BgmSoundPlay(string clipname)
    {

        foreach (var clip in clips)
        {
            if (clip.name == clipname)
            {
                bgmaudio.clip = clip;
                bgmaudio.Play();

                return;
            }
        }
        Debug.Log($"{clipname}을 찾지 못했습니다.");
    }

    public void EventSoundPlay(string clipname)
    {
        foreach (var clip in clips)
        {
            if (clip.name == clipname)
            {                
                eventAudio.PlayOneShot(clip);

                return;
            }
        }

        Debug.Log($"{clipname}을 찾지 못했습니다.");
    }

    public void BgmSoundsPause()
    {
        bgmaudio.Pause();
    }

    public void BgmSoundsResume()
    {
        bgmaudio.UnPause();
    }

    public void BgmSoundStop()
    {
        bgmaudio.Stop();
    }

    void OnBgmVolumeChange(float volume)
    {
        bgmaudio.volume = volume;
    }
    void OnEventVolumeChange(float volume)
    {
        eventAudio.volume = volume;
    }
    
    public void Intro()
    {
        BgmSoundPlay("intro");
    }

    public void GamePlayBGM()
    {
        BgmSoundPlay("newBGM1");
    }

    public void ClickEvent()
    {
        EventSoundPlay("click");
    }
}

