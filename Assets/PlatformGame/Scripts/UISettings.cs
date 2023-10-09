using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PlatformGame
{

    public class UISettings : CustomBehaviur
    {
        public Toggle MuteMusicToggle;
        public Toggle MuteSfxToggle;

        public Slider MusicVolumeSlider;
        public Slider SfxVolumeSlider;

        public GameObject OptionsPanel;
        public GameObject LevelCompletePanel;
        // Start is called before the first frame update
    

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);

            if (gameManager.audioPlayer.IsMusicMuted)
                MuteMusicToggle.isOn = true;
            else
                MuteMusicToggle.isOn = false;

            if (gameManager.audioPlayer.IsSoundMuted)
                MuteSfxToggle.isOn = true;
            else
                MuteSfxToggle.isOn = false;

            MusicVolumeSlider.value = gameManager.audioPlayer.MusicVolume;
            SfxVolumeSlider.value = gameManager.audioPlayer.SoundValume;

            MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
            SfxVolumeSlider.onValueChanged.AddListener(delegate { SfxVolumeChange(); });

            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteMusic(); });
            MuteSfxToggle.onValueChanged.AddListener(delegate { MuteSfx(); });

            OptionsPanel.SetActive(false);
            LevelCompletePanel.SetActive(false);

            _gameManager.OnLevelComplated += OpenLevelFinishedPanel;
        }

        private void OnDestroy()
        {
            _gameManager.OnLevelComplated += OpenLevelFinishedPanel;
        }

        private void OpenLevelFinishedPanel()
        {
            LevelCompletePanel.SetActive(true);
        }

        public void MusicVolumeChange()
        {
            _gameManager.audioPlayer.MusicVolume = MusicVolumeSlider.value;
        }

        public void SfxVolumeChange()
        {
            _gameManager.audioPlayer.SoundValume = SfxVolumeSlider.value;
        }

        public void MuteMusic()
        {
            _gameManager.audioPlayer.IsMusicMuted = MuteMusicToggle.isOn;
        }
        public void MuteSfx()
        {
            _gameManager.audioPlayer.IsSoundMuted = MuteSfxToggle.isOn;
        }

        public void GotoNextLevel()
        {
            _gameManager.GoToNextLevel();
            LevelCompletePanel.SetActive(false);
        }
    }
}