using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource bgmaudio;
    [SerializeField] AudioSource eventAudio;

    [SerializeField] AudioClip[] clips;

    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider eventVolume;


    public bool isGameEnd = false;
    private void Awake() //초기화 작업 
    {
        bgmVolume.value = bgmaudio.volume; //현재 오디오 볼륨을 슬라이더값으로
        eventVolume.value = eventAudio.volume;
    }

    private void Start()
    {
        BgmSoundPlay("Gb 1");
        bgmVolume.onValueChanged.AddListener(OnBgmVolumeChange);
        eventVolume.onValueChanged.AddListener(OnEventVolumeChange);
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

    void OnBgmVolumeChange(float volume)
    {
        bgmaudio.volume = volume;
    }
    void OnEventVolumeChange(float volume)
    {
        eventAudio.volume = volume;
    }
}

