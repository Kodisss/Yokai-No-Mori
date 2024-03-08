using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KawaiiDesu
{
    public class Graveyard : MonoBehaviour
    {
        private List<Transform> anchorList = new List<Transform>();

        private void Start()
        {
            anchorList = transform.GetComponentsInChildren<Transform>().ToList<Transform>();
        }

        public void CapturedPiece(PieceBehavior piece)
        {
            piece.transform.parent = GetFirstTransformAvailable();
            piece.transform.localPosition = Vector2.zero;
        }

        private Transform GetFirstTransformAvailable()
        {
            foreach (Transform t in anchorList)
            {
                if (t.childCount ==  0) return t;
            }
            return null;
        }
    }
}