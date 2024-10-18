using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client
{
    public class BoardBehaviour : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private PieceMovementHolder movementHolder;

        private IBoard board;
        private IMatch match;
        private PieceBehaviour selectedPiece;
        private InputManager input;

        public void Setup(IMatch matchSetup, InputManager inputSetup)
        {
            input = inputSetup;
            board = matchSetup.Board;
            match = matchSetup;
            match.OnEnd += OnEndMatch;

            input.Setup(this);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            Vector3 clickPosition = input.GetClickPosition(cam);
            Vector2Int location = TatedrezUtils.PositionToGridLocation(clickPosition);
            Collider2D collider = Physics2D.Raycast(clickPosition, Vector3.forward).collider;

            if (collider == null)
                return;

            if (collider.gameObject.layer == LayerMask.NameToLayer("Piece"))
            {
                SelectPiece(collider.GetComponent<PieceBehaviour>());
                return;
            }

            if (selectedPiece != null)
            {
                if (selectedPiece.TryMove(board, location))
                    match.CurrentPlayer.CloseTurn(match);
            }

            selectedPiece?.Unselect();
            selectedPiece = null;
            movementHolder.OnSelect(board, selectedPiece);
        }

        private void SelectPiece(PieceBehaviour pieceBhv)
        {
            if (match.CurrentPlayer.Id == pieceBhv.Piece.OwnerId && !(pieceBhv.Piece.IsLocated && match.CurrentPlayer.PendingPieces.Count > 0))
            {
                selectedPiece?.Unselect();
                selectedPiece = pieceBhv;
                selectedPiece?.Select();
                movementHolder.OnSelect(board, selectedPiece);
                Debug.Log($"[Client/Board] - Select Piece {selectedPiece}");
            }
        }

        private void OnEndMatch(IPlayer victoryPlayer)
        {
            input.Disable();
        }
    }
}