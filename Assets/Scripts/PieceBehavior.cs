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
        public SpriteRenderer PieceSprite { get => _pieceSprite; set => _pieceSprite = value; }
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
                if (_isCaptured)
                {
                    BoardManager.instance.ParachutableCells();
                }
                else
                {
                    transform.GetComponentInParent<PhysicalCells>().ShowColorsAround(this);
                }
                BoardManager.instance.AllColliders(false, this);
                _selected = true;
            }
            else
            {
                BoardManager.instance.HideAllSpriteRenderer();
                BoardManager.instance.AllColliders(true, this);
                _selected = false;
            }
        }
    }
}

