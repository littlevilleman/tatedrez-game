using UnityEngine;

namespace Core
{
    public delegate void BuildMatch(IMatch match, IBoard board);
    public delegate void StartMatch();
    public delegate void EndMatch(IPlayer victoryPlayer);
    public delegate void SwitchTurn(IPlayer currentPlayer); 
    public delegate void LocatePiece(Vector2Int location);
}