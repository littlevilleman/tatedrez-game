using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Input
{
    public interface IMatchInputObserver
    {
        public void OnClick(InputAction.CallbackContext context);
    }

    public class MatchInputHandler : IMatchInputObserver
    {
        private IMatch match;
        private IMatchRequestHandler requestHandler;
        private IMatchPieceSelector pieceSelector;
        private InputManager input;

        public MatchInputHandler(IMatch matchSetup, IMatchPieceSelector pieceSelectorSetup, InputManager inputSetup)
        {
            match = matchSetup;
            input = inputSetup;
            pieceSelector = pieceSelectorSetup;
            requestHandler = match.LocalPlayer;

            match.OnEnd += OnEndMatch;
            input.AddObserver(this);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            Vector3 clickPosition = input.GetClickPosition();
            IPiece piece = GetPiece(clickPosition);

            if (TatedrezUtils.IsValidSelection(requestHandler, piece))
            {
                pieceSelector.Select(piece);
                return;
            }

            if (pieceSelector.SelectedPiece != null)
            {
                Vector2Int location = TatedrezUtils.PositionToGridLocation(clickPosition);
                requestHandler.RequestMove(match, pieceSelector.SelectedPiece, location);
            }

            pieceSelector.Select(null);
        }

        private IPiece GetPiece(Vector3 clickPosition)
        {
            Collider2D collider = Physics2D.Raycast(clickPosition, Vector3.forward).collider;

            if (collider == null || collider.gameObject.layer != LayerMask.NameToLayer("Piece"))
                return null;

            return collider.GetComponent<PieceBehaviour>().Piece;
        }

        private void OnEndMatch(IPlayer victoryPlayer)
        {
            match.OnEnd -= OnEndMatch;
            input.RemoveObserver(this);
        }
    }
}