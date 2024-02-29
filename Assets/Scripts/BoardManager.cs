using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KawaiiDesu
{
    public class BoardManager : MonoBehaviour
    {
        public PiecesSO[,] board = new PiecesSO[3, 4];
        public bool[,] isOccupied = new bool[3, 4];

        public Transform[,] cells = new Transform[3, 4];

        [SerializeField] private PieceBehavior piecePrefab;
        [SerializeField] private Transform cellsParent;
        [SerializeField] private float snapRange;

        [Header("All lists to initialize the board")]
        [SerializeField] private List<Transform> cellList = new List<Transform>();
        [SerializeField] private List<PiecesSO> piecesPositionList = new List<PiecesSO>();

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
                    board[i,j] = piecesPositionList[index];

                    if (board[i, j] != null)
                    {
                        PieceBehavior newPiece = Instantiate(piecePrefab, cells[i, j]);
                        newPiece.PieceData = board[i, j];
                        if (j > 1) newPiece.Side = false; //turn the piece if you are opponent
                        newPiece.Init();
                        newPiece.dragEndDelegate = SnapObject;
                    }

                    index++;
                }
            }
        }

        public void CheckPiecesPositions()
        {
            List<Transform> child = cellsParent.GetComponentsInChildren<Transform>().ToList<Transform>();

            child.RemoveAll(x => x.tag != "Cell");

            for(int i = 0; i < child.Count; i++)
            {
                if (child[i].childCount != 0)
                {
                    piecesPositionList[i] = child[i].GetComponentInChildren<PieceBehavior>().PieceData;
                }
                else
                {
                    piecesPositionList[i] = null;
                }
            }
        }

        public void SnapObject(Transform obj)
        {
            foreach (Transform point in cellList)
            {
                if (Vector2.Distance(point.position, obj.position) <= snapRange)
                {
                    obj.parent = null;
                    obj.parent = point;
                    obj.localPosition = Vector2.zero;
                    CheckPiecesPositions();
                    return;
                }
            }
        }
    }
}

