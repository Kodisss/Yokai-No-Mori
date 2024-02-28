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
        public string pieceName { get => _pieceName;}

        [SerializeField] private List<DirectionsData> _directions = new List<DirectionsData>(8);
        public List<DirectionsData> directions { get => _directions;}

        [SerializeField] private bool _isPromoted = false;
        public bool isPromoted { get => _isPromoted;}

        [SerializeField] private int[][] _coordinates;
        public int[][] coordinates { get => _coordinates;}

        [SerializeField] private Sprite _sprite;
        public Sprite sprite { get => _sprite;}

    }

    [Serializable]
    public class DirectionsData
    {
        [SerializeField] private string _name;
        public string name { get => _name;}

        [SerializeField] private bool _canGo;
        public bool canGo { get => _canGo;}
    }
}