using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound_BGM;
    public AudioSource sound_Button;
    public Slider musicVolumeSlider; // ��� ���� ������ �����ϴ� �����̴�
    public Slider effectVolumeSlider; // ��ư ȿ���� ������ �����ϴ� �����̴�
    public Toggle muteToggle; // ���Ұ� ��ư
    private float originalBGMVolume; // ���� ��� ���� ���� ���� ������ ����
    private float originalButtonVolume; // ���� ��ư ȿ���� ���� ���� ������ ����
    public bool isMute; // ���Ұ� �������� �ƴ���

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume") && PlayerPrefs.HasKey("ButtonVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            sound_BGM.volume = bgmVolume;
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume");
            sound_Button.volume = buttonVolume;
        }

        if (PlayerPrefs.HasKey("BGMVolume_original") && PlayerPrefs.HasKey("ButtonVolume_original"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume_original");
            sound_BGM.volume = bgmVolume;
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume_original");
            sound_Button.volume = buttonVolume;
        }

        muteToggle.isOn = isMute;
        originalBGMVolume = sound_BGM.volume;
        originalButtonVolume = sound_Button.volume;
        musicVolumeSlider.value = originalBGMVolume;    // �� ��ȯ �� �����̴��� ���� volume ���� ����
        effectVolumeSlider.value = originalButtonVolume;
    }

    public void SetBgmVolume(float volume)
    {
        sound_BGM.volume = volume;
        musicVolumeSlider.value = volume;
    }

    public void SetEffectVolume(float volume)
    {
        sound_Button.volume = volume;
        effectVolumeSlider.value = volume;
    }

    public void Mute()
    {
        if(!isMute)
        {
            PlayerPrefs.SetFloat("BGMVolume_original", sound_BGM.volume);
            PlayerPrefs.SetFloat("ButtonVolume_original", sound_Button.volume);

            sound_BGM.volume = 0f;  
            sound_Button.volume = 0f;
            musicVolumeSlider.value = 0f;
            effectVolumeSlider.value = 0f;

            isMute = true;
        }
        else
        {
            sound_BGM.volume = PlayerPrefs.GetFloat("BGMVolume_original");
            sound_Button.volume = PlayerPrefs.GetFloat("ButtonVolume_original");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume_original");
            effectVolumeSlider.value = PlayerPrefs.GetFloat("ButtonVolume_original");

            isMute = false;
        }
        
    }


    public void OnSoundButton() // ��ư Ŭ���� ���� ���
    {
        sound_Button.Play();
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", sound_BGM.volume);
        PlayerPrefs.SetFloat("ButtonVolume", sound_Button.volume);
    }

    public void LoadSoundSettings()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            sound_BGM.volume = bgmVolume;
            musicVolumeSlider.value = bgmVolume;
        }

        if (PlayerPrefs.HasKey("ButtonVolume"))
        {
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume");
            sound_Button.volume = buttonVolume;
            effectVolumeSlider.value = buttonVolume;
        }
    }
}