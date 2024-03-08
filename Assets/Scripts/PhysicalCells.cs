using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KawaiiDesu
{
    public class PhysicalCells : MonoBehaviour
    {
        [SerializeField] private List<PhysicalCells> surroundingCells = new List<PhysicalCells>(8);

        [HideInInspector] public SpriteRenderer spriteRenderer;

        private bool _canMove = false;
        private bool _canParachute = false;
        private bool _hasPiece = false;
        public bool HasPiece { get => _hasPiece; set => _hasPiece = value; }
        public bool CanParachute { get => _canParachute; set => _canParachute = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }

        public void ShowColorsAround(PieceBehavior piece)
        {
            //Debug.Log(name);
            List<bool> possibleMove = new List<bool>();

            if (piece.Side)
            {
                possibleMove = piece.PieceData.Directions;
            }
            else
            {
                possibleMove = piece.PieceData.DirectionsUpsideDown;
            }

            for(int i = 0; i < 8; i++)
            {
                PhysicalCells cell = surroundingCells[i];
                if (cell != null)
                {
                    if (possibleMove[i])
                    {
                        cell.CanMove = true;
                        if (cell.HasPiece)
                        {
                            cell.spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f);
                            cell.spriteRenderer.enabled = true;
                        }
                        else
                        {
                            cell.spriteRenderer.color = new Color(0f, 1f, 0f, 0.4f);
                            cell.spriteRenderer.enabled = true;
                        }
                    }
                }
            }
        }

        public void HideColorsAround()
        {
            foreach(PhysicalCells cell in surroundingCells)
            {
                if(cell != null) cell.spriteRenderer.enabled = false;
            }
        }

        private void OnMouseUpAsButton()
        {
            BoardManager.instance.MovePiece(this);
        }
    }
}