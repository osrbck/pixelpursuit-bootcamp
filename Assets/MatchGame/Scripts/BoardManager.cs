using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchGame
{
    public class BoardManager : MonoBehaviour
    {
        private GameManager Manager;

        private int _boardWidth;
        private int _boardHeight;
        private int[] _gameBoard;

        public void Init(GameManager gm, int width, int height)
        {
            Manager = gm;
            InitBoard(width, height);
        }
    

        public void InitBoard(int width, int height)
        {
            _boardWidth = width;
            _boardHeight = height;
            _gameBoard = new int[_boardWidth * _boardHeight];
        }
    }
}