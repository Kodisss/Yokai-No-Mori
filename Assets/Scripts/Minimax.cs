using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KawaiiDesu
{
    public enum GameState
    {
        Draw = 0,
        Win = 1,
        Lose = -1
    }

    public class Minimax : MonoBehaviour
    {


        BoardManager _board;

        public int MinimaxAlgo(GameState state, int _depth, bool _maxPlayer)
        {
            if (state == GameState.Win || state == GameState.Lose || state == GameState.Draw)
            {
                return (int)state;
            }

            if (_maxPlayer)
            {
                float value = Mathf.NegativeInfinity;

                foreach (PhysicalCells cells in  _board.cellList)
                {
                    int score = MinimaxAlgo(GameState.Win, _depth, true);
                    value = Mathf.Max(value, score);
                }
                return (int)value;
            }
            else
            {
                float value = Mathf.Infinity;

                foreach (PhysicalCells cells in _board.cellList)
                {
                    int score = MinimaxAlgo(GameState.Win, _depth, false);
                    value = Mathf.Min(value, score);
                }
                return (int)value;
            }

        }


        public void EvaluateGame()
        {

        }

    }
}

