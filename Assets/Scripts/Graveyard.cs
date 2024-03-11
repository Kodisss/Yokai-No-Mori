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
            anchorList.RemoveAll(x => x.tag != "Anchor");
        }

        public void CapturedPiece(PieceBehavior piece)
        {
            piece.transform.parent = GetFirstTransformAvailable();
            piece.transform.localPosition = Vector2.zero;
        }

        public List<PiecesSO> GetAllPiecesInGraveyard()
        {
            List<PiecesSO> res = new List<PiecesSO>();

            foreach (Transform t in anchorList)
            {
                PieceBehavior pieceBehavior = t.GetComponentInChildren<PieceBehavior>();
                if (pieceBehavior != null)
                {
                    res.Add(pieceBehavior.PieceData);
                }
            }

            return res;
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