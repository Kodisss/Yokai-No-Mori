using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KawaiiDesu
{
    public class BoardManager : MonoBehaviour
    {
        public static BoardManager instance;

        [SerializeField] private PieceBehavior piecePrefab;
        [SerializeField] private Transform cellsParent;
        [SerializeField] private float snapRange;

        [Header("All lists to initialize the board")]
        [SerializeField] public List<PhysicalCells> cellList = new List<PhysicalCells>();
        [SerializeField] public List<PieceBehavior> pieceBehaviors = new List<PieceBehavior>();
        [SerializeField] private List<PiecesSO> piecesPositionList = new List<PiecesSO>();

        private void Awake()
        {
            if(instance == null) instance = this;
        }

        void Start()
        {
            cellList = cellsParent.GetComponentsInChildren<PhysicalCells>().ToList<PhysicalCells>();
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
                    pieceBehaviors.Add(newPiece);
                }
                else
                {
                    pieceBehaviors.Add(null);
                }
                index++;
                side++;
                if(side > 3) side = 0;
            }
        }

        public void CheckPiecesPositions()
        {
            int i = 0;
            foreach(PhysicalCells piece in cellList)
            {
                if (piece.transform.childCount != 0)
                {
                    piecesPositionList[i] = piece.transform.GetComponentInChildren<PieceBehavior>().PieceData;
                    piece.HasPiece = true;
                }
                else
                {
                    piecesPositionList[i] = null;
                    piece.HasPiece = true;
                }
                i++;
            }
        }

        public void MovePiece(Transform newParent)
        {
            //Debug.Log("try to move piece on " + newParent.name);
            bool oneSelected = false;
            PieceBehavior selectedPiece = null;

            foreach(PieceBehavior piece in pieceBehaviors)
            {
                if (piece != null && piece.Selected)
                {
                    selectedPiece = piece;
                    oneSelected = true;
                }
            }

            if (!oneSelected) return;

            selectedPiece.transform.parent = null;
            selectedPiece.transform.parent = newParent;
            selectedPiece.transform.localPosition = Vector2.zero;
            selectedPiece.Selected = false;

            foreach(PhysicalCells cell in cellList)
            {
                cell.spriteRenderer.enabled = false;
            }

            CheckPiecesPositions();
        }
    }
}

