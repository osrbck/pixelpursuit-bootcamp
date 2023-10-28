using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchGame
{
    public class GameManager : MonoBehaviour
    {
        public BoardManager Board;

        private void Start()
        {
            Board.Init(this, 6, 6);
        }
    }
}