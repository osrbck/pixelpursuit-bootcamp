using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnLevelStarted;
        public event Action OnLevelComplated;

        [SerializeField] private PlayerController _player;

        [SerializeField] private UISettings _uiSettings;
        public AudioManager audioPlayer;

        [SerializeField] private GameObject[] _levelPrefabs;
        [SerializeField] private Level _currentLevel;
        [SerializeField] private int _idLevel;

        public Level CurrentLevel
        {
            get
            {
                return _currentLevel;
            }
            private set
            {
                _currentLevel = value;
            }
        }

        public void Start()
        {
            _uiSettings.Init(this);
            _idLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            _player.Init(this);
            StartLevel();
        }

        public void StartLevel()
        {
            GameObject goLevel = Instantiate(_levelPrefabs[_idLevel % _levelPrefabs.Length], Vector3.zero, Quaternion.identity);
            _currentLevel = goLevel.GetComponent<Level>();
            _currentLevel.Init(this);

            OnLevelStarted.Invoke();
        }

        public void CheckIfLevelEnded()
        {
            int collectedCoin = _player.Data.isCollected;
            int neededCoin = _currentLevel.MinCoinCollectAmount;

            if(collectedCoin >= neededCoin)
            {
                OnLevelComplated.Invoke();
            }
        }

        public void GoToNextLevel()
        {
            _idLevel += 1;
            _idLevel = PlayerPrefs.GetInt("CurrentLevel", _idLevel);
            Destroy(_currentLevel.gameObject);
            StartLevel();

        }
    }
}