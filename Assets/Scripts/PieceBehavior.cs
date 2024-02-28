using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KawaiiDesu
{
    public class PieceBehavior : MonoBehaviour
    {
        PiecesSO _pieceData;
        SpriteRenderer _pieceSprite;
        public PiecesSO PieceData { get => _pieceData; set => _pieceData = value; }
        public void Init()
        {
            _pieceSprite = GetComponent<SpriteRenderer>();
            _pieceSprite.sprite = _pieceData.sprite;



        }

        
    }
}

