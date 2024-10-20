using Core;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    public class PieceMovementHolder : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> locationHolders;
        [SerializeField] private Color validColor;
        [SerializeField] private Color fadeColor;
        [SerializeField] private Color offColor;
        private Material mat;
        private Tween colorTween;

        private void Awake()
        {
            mat = locationHolders[0].sharedMaterial;
            colorTween = mat.DOColor(fadeColor, .5f).SetLoops(-1, LoopType.Yoyo).SetAutoKill(false).Pause();
        }

        public void OnSelect(IBoard board, IPiece piece)
        {
            foreach (SpriteRenderer holder in locationHolders)
                holder.gameObject.SetActive(false);
            
            colorTween.Rewind();

            if (piece == null)
                return;

            Vector2Int[] moves = piece.GetValidMoves(board).ToArray();
            for (int i = 0; i < moves.Length; i++)
            {
                locationHolders[i].transform.position = new Vector3(moves[i].x + .5f, moves[i].y + .5f);
                locationHolders[i].gameObject.SetActive(true);
            }

            if (!colorTween.IsPlaying())
            {
                mat.color = fadeColor;
                colorTween.Restart();
            }
        }
    }
}