using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace KawaiiDesu
{
    public class PieceBehavior : MonoBehaviour
    {
        public delegate void DragEndDelegate(Transform transform, PieceBehavior behavior);
        public DragEndDelegate dragEndDelegate;

        private PiecesSO _pieceData;
        private SpriteRenderer _pieceSprite;
        private bool _side = true;
        private bool _selected = false;
        private bool _isCaptured = false;

        public bool Side { get => _side; set => _side = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public PiecesSO PieceData { get => _pieceData; set => _pieceData = value; }

        public void Init()
        {
            _pieceSprite = GetComponent<SpriteRenderer>();
            _pieceSprite.sprite = _pieceData.Sprite;
            if (!_side)
            {
                transform.Rotate(new Vector3(0, 0, 180));
            }
        }

        private void OnMouseUpAsButton()
        {
            foreach(PieceBehavior piece in BoardManager.instance.pieceBehaviors)
            {
                if(piece != null && piece != this)
                {
                    if (piece.Selected) return;
                }
            }

            if (!_selected)
            {
                transform.GetComponentInParent<PhysicalCells>().ShowColorsAround(this);
                _selected = true;
            }
            else
            {
                transform.GetComponentInParent<PhysicalCells>().HideColorsAround();
                _selected = false;
            }
        }
    }
}

