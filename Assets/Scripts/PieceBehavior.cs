using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace KawaiiDesu
{
    public class PieceBehavior : MonoBehaviour
    {
        public delegate void DragEndDelegate(Transform transform);
        public DragEndDelegate dragEndDelegate;

        private PiecesSO _pieceData;
        private SpriteRenderer _pieceSprite;
        private bool _side = true;
        private bool _isCaptured = false;
        private Vector3 offset;
        private bool _isDragging = false;

        BoardManager _board;

        public bool Side { get => _side; set => _side = value; }
        public PiecesSO PieceData { get => _pieceData; set => _pieceData = value; }

        public void Init()
        {
            _pieceSprite = GetComponent<SpriteRenderer>();
            _pieceSprite.sprite = _pieceData.sprite;
            if (!_side)
            {
                transform.Rotate(new Vector3(0, 0, 180));
            }
        }

        void OnMouseDown()
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _isDragging = true;
        }

        void OnMouseUp()
        {
            _isDragging = false;
            dragEndDelegate(this.transform);
        }

        void Update()
        {
            if (_isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            }
        }

        
    }
}

