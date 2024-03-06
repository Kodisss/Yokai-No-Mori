using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KawaiiDesu
{
    [Serializable]
    public struct Cell
    {
        public Cell(Transform t, bool b)
        {
            transform = t;
            isOccupied = b;
        }

        public Transform transform;
        public bool isOccupied;
    }

    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private PieceBehavior piecePrefab;
        [SerializeField] private Transform cellsParent;
        [SerializeField] private float snapRange;

        [Header("All lists to initialize the board")]
        [SerializeField] private List<Cell> cellList = new List<Cell>();
        [SerializeField] private List<PiecesSO> piecesPositionList = new List<PiecesSO>();

        void Start()
        {
            InitializeListOfCells();
            InitializeAllGrids();
        }

        private void InitializeAllGrids()
        {
            int index = 0;
            int side = 0;

            foreach (PiecesSO piece in piecesPositionList)
            {
                if(piece != null)
                {
                    PieceBehavior newPiece = Instantiate(piecePrefab, cellList[index].transform);
                    newPiece.PieceData = piece;
                    if (side > 1) newPiece.Side = false; //turn the piece if you are opponent
                    newPiece.Init();
                    newPiece.dragEndDelegate = SnapObject;
                }
                index++;
                side++;
                if(side > 3) side = 0;
            }
        }

        private void InitializeListOfCells()
        {
            List<Transform> child = cellsParent.GetComponentsInChildren<Transform>().ToList<Transform>();

            child.RemoveAll(x => x.tag != "Cell");

            foreach (Transform cell in child)
            {
                Cell tempCell = new Cell();
                tempCell.transform = cell;
                cellList.Add(tempCell);
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
                    cellList[i] = new Cell(child[i], true);
                }
                else
                {
                    piecesPositionList[i] = null;
                    cellList[i] = new Cell(child[i], false);
                }
            }
        }

        public void SnapObject(Transform obj)
        {
            foreach (Cell point in cellList)
            {
                if (Vector2.Distance(point.transform.position, obj.position) <= snapRange)
                {
                    if (point.isOccupied/*&& allied*/)
                    {
                        obj.localPosition = Vector2.zero;
                        return;
                    }
                    obj.parent = null;
                    obj.parent = point.transform;
                    obj.localPosition = Vector2.zero;
                    CheckPiecesPositions();
                    return;
                }
            }
        }
    }
}

