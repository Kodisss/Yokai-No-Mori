using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KawaiiDesu
{
    public class PieceBehavior : MonoBehaviour
    {
        private PiecesSO _pieceData;
        private SpriteRenderer _pieceSprite;
        private bool _side = true;
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
    }
}

