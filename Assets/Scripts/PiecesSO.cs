using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace KawaiiDesu
{
    [CreateAssetMenu(menuName = "Pieces")]
    public class PiecesSO : ScriptableObject
    {
        [SerializeField] private string _pieceName;

        [SerializeField] private List<DirectionsData> _directions = new List<DirectionsData>(8);

        [SerializeField] private bool _isPromoted = false;

        [SerializeField] private int[][] _coordinates;

        [SerializeField] private Sprite _sprite;

        [SerializeField] private Sprite _promotedSprite;
    }

    [Serializable]
    public class DirectionsData
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _canGo;
    }
}