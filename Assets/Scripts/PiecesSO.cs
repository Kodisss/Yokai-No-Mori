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
        public string PieceName { get => _pieceName;}

        [SerializeField] private List<bool> _directions = new List<bool>(8);
        public List<bool> Directions { get => _directions; }

        [SerializeField] private List<bool> _directionsUpsideDown = new List<bool>(8);
        public List<bool> DirectionsUpsideDown { get => _directionsUpsideDown; }

        [SerializeField] private bool _canPromote = false;
        public bool CanPromote { get => _canPromote; }

        [SerializeField] private bool _canWin = false;
        public bool CanWin { get => _canWin; }

        [SerializeField] private int[][] _coordinates;
        public int[][] Coordinates { get => _coordinates;}

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite { get => _sprite;}

    }
}