using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource bgmaudio;
    [SerializeField] AudioSource eventAudio;

    [SerializeField] AudioClip[] clips;

    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider eventVolume;

    [SerializeField] Toggle bgmMute;
    [SerializeField] Toggle eventMute;

    private void Awake() //초기화 작업 
    {

        //bgmVolume.value = bgmaudio.volume; //현재 오디오 볼륨을 슬라이더값으로
        //eventVolume.value = eventAudio.volume;

        //bgmMute.isOn = bgmaudio.mute;  
        //eventMute.isOn = eventAudio.mute;
    }

    private void Start()
    {
        BgmSoundPlay("Gb 1");
        //bgmVolume.onValueChanged.AddListener(OnBgmVolumeChange);
        //eventVolume.onValueChanged.AddListener(OnEventVolumeChange);

        //bgmMute.onValueChanged.AddListener(OnBgmMute);
        //eventMute.onValueChanged.AddListener(OnEventMute);
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
                eventAudio.volume = 0.4f;
                eventAudio.PlayOneShot(clip);

                return;
            }
        }

        Debug.Log($"{clipname}을 찾지 못했습니다.");
    }

    //void OnBgmVolumeChange(float volume)
    //{
    //    bgmaudio.volume = volume;
    //}
    //void OnEventVolumeChange(float volume)
    //{
    //    eventAudio.volume = volume;
    //}

    //void OnBgmMute(bool isMute) 
    //{
    //    bgmaudio.mute = isMute;
    //}

    //void OnEventMute(bool isMute)
    //{
    //    eventAudio.mute = isMute;
    //}
}

