using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class CustomBehaviur : MonoBehaviour
    {
        protected GameManager _gameManager;
        public virtual void Init(GameManager gm)
        {
            _gameManager = gm;
        }
    }
}