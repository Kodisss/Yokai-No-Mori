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

        [SerializeField] private PiecesSO samurai;

        [SerializeField] private PieceBehavior piecePrefab;
        [SerializeField] private Transform cellsParent;
        [SerializeField] private Graveyard graveyardUp;
        [SerializeField] private Graveyard graveyardDown;

        [Header("All lists to initialize the board")]
        [SerializeField] public List<PhysicalCells> cellList = new List<PhysicalCells>();
        [SerializeField] private List<PhysicalCells> promotingCellsUp = new List<PhysicalCells>();
        [SerializeField] private List<PhysicalCells> promotingCellsDown = new List<PhysicalCells>();
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

            CheckPiecesPositions();
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
                    piece.HasPiece = false;
                }
                i++;
            }
        }

        public void MovePiece(PhysicalCells newCell)
        {
            // si on peut pas aller ici on sort
            if (!newCell.CanMove) return;

            Transform newParent = newCell.transform;
            //Debug.Log("try to move piece on " + newParent.name);
            bool oneSelected = false;
            PieceBehavior selectedPiece = null;

            // on check la piece selectionnée
            foreach(PieceBehavior piece in pieceBehaviors)
            {
                if (piece != null && piece.Selected)
                {
                    selectedPiece = piece;
                    oneSelected = true;
                }
            }

            // si aucune pièce selectionnée on sort
            if (!oneSelected) return;

            // on check si on doit capturer et on bouge la pièce capturée en fonction
            if (newCell.HasPiece)
            {
                PieceBehavior otherPiece = newCell.GetComponentInChildren<PieceBehavior>();
                if (otherPiece.Side == selectedPiece.Side)
                {
                    return;
                }
                else
                {
                    otherPiece.IsCaptured = true;
                    otherPiece.transform.parent = null;
                    if (otherPiece.Side)
                    {
                        graveyardUp.CapturedPiece(otherPiece);
                    }
                    else
                    {
                        graveyardDown.CapturedPiece(otherPiece);
                    }
                }
            }

            // on bouge la pièce en fonction
            selectedPiece.transform.parent = null;
            selectedPiece.transform.parent = newParent;
            selectedPiece.transform.localPosition = Vector2.zero;
            selectedPiece.Selected = false;
            selectedPiece.AllColliders(true);

            if (selectedPiece.IsCaptured)
            {
                selectedPiece.IsCaptured = false; // reset the captured state
            }
            else
            {
                PromotePiece(selectedPiece); // si elle vient pas d'être parachutée check si elle doit être promue
            }

            foreach (PhysicalCells cell in cellList)
            {
                cell.spriteRenderer.enabled = false;
            }

            CheckPiecesPositions();
            ResetCellsStates();
        }

        public void HideAllSpriteRenderer()
        {
            foreach (PhysicalCells cell in cellList)
            {
                cell.spriteRenderer.enabled = false;
            }
        }

        public void ParachutableCells()
        {
            foreach (PhysicalCells cell in cellList)
            {
                if (!cell.HasPiece)
                {
                    cell.CanMove = true;
                    cell.spriteRenderer.color = new Color(0f, 0f, 1f, 0.4f);
                    cell.spriteRenderer.enabled = true;
                }
            }
        }

        private void PromotePiece(PieceBehavior piece)
        {
            if (piece.Side)
            {
                foreach(PhysicalCells cell in promotingCellsUp)
                {
                    if (cell.GetComponentInChildren<PieceBehavior>() == piece)
                    {
                        Promote(piece);
                    }
                }
            }
            else
            {
                foreach(PhysicalCells cell in promotingCellsDown)
                {
                    if (cell.GetComponentInChildren<PieceBehavior>() == piece)
                    {
                        Promote(piece);
                    }
                }
            }
        }

        private void Promote(PieceBehavior piece)
        {
            if (piece.PieceData.CanPromote)
            {
                piece.PieceData = samurai;
                piece.Init();
            }
        }

        private void ResetCellsStates()
        {
            foreach(PhysicalCells cell in cellList)
            {
                cell.CanMove = false;
                cell.CanParachute = false;
            }
        }
    }
}

