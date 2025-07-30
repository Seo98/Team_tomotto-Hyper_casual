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

        //bgmMute.isOn = bgmaudio.mute;  //isOn이나 value 모두 인스펙터 안에 있는 거 접근한 것
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
                eventAudio.PlayOneShot(clip);//실행하면 제어 x 

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

    //void OnBgmMute(bool isMute) //인게임에서 누른 게 여기 매개변수로 들어와서 값을 바로 바꿔줌.
    //{
    //    bgmaudio.mute = isMute;
    //}

    //void OnEventMute(bool isMute)
    //{
    //    eventAudio.mute = isMute;
    //}
}

