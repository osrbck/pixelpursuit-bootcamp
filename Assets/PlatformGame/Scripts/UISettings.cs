using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PlatformGame
{

    public class UISettings : MonoBehaviour
    {
        public Toggle MuteMusicToggle;
        public Toggle MuteSfxToggle;

        public Slider MusicVolumeSlider;
        public Slider SfxVolumeSlider;

        public GameObject OptionsPanel;
        // Start is called before the first frame update
        void Start()
        {
            if (AudioManager.Instance.IsMusicMuted)
                MuteMusicToggle.isOn = true;
            else
                MuteMusicToggle.isOn = false;

            if (AudioManager.Instance.IsSoundMuted)
                MuteSfxToggle.isOn = true;
            else
                MuteSfxToggle.isOn = false;

            MusicVolumeSlider.value = AudioManager.Instance.MusicVolume;
            SfxVolumeSlider.value = AudioManager.Instance.SoundValume;

            MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange();});
            SfxVolumeSlider.onValueChanged.AddListener(delegate { SfxVolumeChange(); });

            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteMusic(); });
            MuteSfxToggle.onValueChanged.AddListener(delegate { MuteSfx(); }); 

            OptionsPanel.SetActive(false);

        }

        public void MusicVolumeChange()
        {
            AudioManager.Instance.MusicVolume = MusicVolumeSlider.value;
        }

        public void SfxVolumeChange()
        {
            AudioManager.Instance.SoundValume = SfxVolumeSlider.value;
        }

        public void MuteMusic()
        {
            AudioManager.Instance.IsMusicMuted = MuteMusicToggle.isOn;
        }
        public void MuteSfx()
        {
            AudioManager.Instance.IsSoundMuted = MuteSfxToggle.isOn;
        }
    }
}