using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    public class PieceMovementHolder : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> locationHolders;
        [SerializeField] private Color validDebugColor;

        public void OnSelect(IBoard board, IPiece piece)
        {
            foreach (SpriteRenderer holder in locationHolders)
                holder.color = new Color(1f, 1f, 1f, 0f);

            if (piece == null)
                return;

            Vector2Int[] moves = piece.GetValidMoves(board).ToArray();
            for (int i = 0; i < moves?.Length; i++)
            {
                locationHolders[i].transform.position = new Vector3(moves[i].x, moves[i].y);
                locationHolders[i].color = validDebugColor;
            }
        }
    }
}