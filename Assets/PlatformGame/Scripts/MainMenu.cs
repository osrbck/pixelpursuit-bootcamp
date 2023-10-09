using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformGame
{

    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private string _sceneName;

        // Start is called before the first frame update
        //void StartGame()
        //{
        //    SceneManager.LoadScene("Platformer");
        //}

        //// Update is called once per frame
        //void QuitGame()
        //{
        //    Application.Quit();
        //}

        public void changeScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}