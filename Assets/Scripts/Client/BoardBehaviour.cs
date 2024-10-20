using Config;
using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    public interface IMatchPieceSelector
    {
        public IPiece SelectedPiece { get; }
        public void Select(IPiece piece);
    }

    public class BoardBehaviour : MonoBehaviour, IMatchPieceSelector
    {
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer gridRenderer;
        [SerializeField] private PieceMovementHolder movementHolder;
        [SerializeField] private Transform[] playerPiecesHolders;

        private IBoard board;
        private ICollection<PieceBehaviour> piecesBhv = new List<PieceBehaviour>();
        public IPiece SelectedPiece { get; private set; }
        private PieceBehaviour SelectedPieceBhv => piecesBhv.Where(x => x.Piece == SelectedPiece).FirstOrDefault();

        public void Setup(IBoard boardSetup, IPlayer[] players, PiecePool piecePool, IGameConfig config)
        {
            board = boardSetup;
            gridRenderer.size = Vector2.one * board.Size;
            cam.transform.position = new Vector3(board.Size / 2f, board.Size / 2f, -10f);

            foreach (IPlayer player in players)
                SetupPlayerPieces(player, piecePool, config);
        }

        public void Select(IPiece piece)
        {
            SelectedPieceBhv?.Unselect();
            SelectedPiece =  piece;
            SelectedPieceBhv?.Select();
            
            movementHolder.OnSelect(board, SelectedPiece);
        }

        private void SetupPlayerPieces(IPlayer player, PiecePool pool, IGameConfig config)
        {
            for (int iPiece = 0; iPiece < player.Pieces.Length; iPiece++)
            {
                IPiece piece = player.Pieces[iPiece];
                PieceBehaviour pieceBhv = pool.Pull(playerPiecesHolders[player.Id]);
                pieceBhv.Setup(piece, iPiece, config.GetPieceConfig(piece.Id));
                piecesBhv.Add(pieceBhv);
            }
        }

        public void Recycle(PiecePool pool)
        {
            foreach (PieceBehaviour pieceBhv in piecesBhv)
                pieceBhv.Recycle(pool);

            piecesBhv.Clear();
        }
    }
}