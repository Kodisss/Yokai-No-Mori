using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace KawaiiDesu
{
    public class PieceBehavior : MonoBehaviour
    {
        private PiecesSO _pieceData;
        private SpriteRenderer _pieceSprite;
        private Collider2D _pieceCollider;
        private bool _side = true;
        private bool _selected = false;
        private bool _isCaptured = false;

        public bool Side { get => _side; set => _side = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public PiecesSO PieceData { get => _pieceData; set => _pieceData = value; }
        public Collider2D PieceCollider { get => _pieceCollider; set => _pieceCollider = value; }
        public bool IsCaptured { get => _isCaptured; set => _isCaptured = value; }

        public void Init()
        {
            _pieceCollider = GetComponent<Collider2D>();
            _pieceSprite = GetComponent<SpriteRenderer>();
            _pieceSprite.sprite = _pieceData.Sprite;
            if (!_side)
            {
                transform.Rotate(new Vector3(0, 0, 180));
            }
        }

        private void OnMouseUpAsButton()
        {
            if (!_selected)
            {
                transform.GetComponentInParent<PhysicalCells>().ShowColorsAround(this);
                AllColliders(false);
                _selected = true;
            }
            else
            {
                transform.GetComponentInParent<PhysicalCells>().HideColorsAround();
                AllColliders(true);
                _selected = false;
            }
        }

        public void AllColliders(bool value)
        {
            foreach (PieceBehavior piece in BoardManager.instance.pieceBehaviors)
            {
                if (piece != null && piece != this)
                {
                    piece.PieceCollider.enabled = value;
                }
            }
        }
    }
}

