using Config;
using Core;
using DG.Tweening;
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
            spriteRenderer.sprite = config.Sprite;
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

        private void OnLocatePiece(Vector2Int location)
        {
            locateTween = transform.DOMove(new Vector3(location.x, location.y), .25f);
        }

        public void Recycle(Pool<PieceBehaviour> pool)
        {
            pool.Recycle(this);
        }
    }
}