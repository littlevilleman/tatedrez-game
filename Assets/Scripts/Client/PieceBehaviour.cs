using Config;
using Core;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class PieceBehaviour : MonoBehaviour, IPoolable<PieceBehaviour>
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public IPiece Piece { get; private set; }
        private Tween selectTween;
        private Tween locateTween;

        public void Setup(IPiece pieceSetup, int index, PieceConfig config)
        {
            Piece = pieceSetup;
            Piece.OnLocate += OnLocatePiece;

            name = config.name;
            spriteRenderer.sprite = config.sprite;
            spriteRenderer.color = Piece.OwnerId == 0 ? Color.white : Color.black;

            transform.localPosition = Vector3.right * index;
            gameObject.SetActive(true);
        }

        public void Select()
        {
            selectTween = spriteRenderer.transform.DOScale(.9f, .5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void Unselect()
        {
            selectTween.Complete();
            selectTween.Rewind();
        }

        public bool TryMove(IBoard board, Vector2Int location)
        {
            return board.TryMovePiece(Piece, location);
        }

        private void OnLocatePiece(IPiece piece, EMoveResult result)
        {
            locateTween = transform.DOMove(new Vector3(piece.Location.x, piece.Location.y), .25f);
        }

        public void Recycle(Pool<PieceBehaviour> pool)
        {
            pool.Recycle(this);
        }

        public IEnumerable<Vector2Int> GetMoves(IBoard board)
        {
            return Piece.GetValidMoves(board);
        }
    }
}