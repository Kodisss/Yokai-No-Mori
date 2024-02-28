using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KawaiiDesu
{
    public class BoardManager : MonoBehaviour
    {
        public PiecesSO[,] board = new PiecesSO[3, 4];
        public bool[,] isOccupied = new bool[3, 4];

        public Transform[,] cells = new Transform[3, 4];

        [SerializeField] private PieceBehavior piecePrefab;

        [Header("All lists to initialize the board")]
        [SerializeField] private List<Transform> cellList = new List<Transform>();
        [SerializeField] private List<PiecesSO> piecesInitialPositionList = new List<PiecesSO>();

        void Start()
        {
            InitializeAllGrids();
        }

        private void InitializeAllGrids()
        {
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    cells[i,j] = cellList[index];
                    board[i,j] = piecesInitialPositionList[index];

                    if (board[i, j] != null)
                    {
                        PieceBehavior newPiece = Instantiate(piecePrefab, cells[i, j]);
                        newPiece.PieceData = board[i, j];
                        if (j > 1) newPiece.Side = false; //turn the piece if you are opponent
                        newPiece.Init();
                    }

                    index++;
                }
            }
        }
    }
}

