namespace Core
{
    public delegate void BuildMatch(IMatch match);
    public delegate void StartMatch();
    public delegate void EndMatch(IPlayer victoryPlayer);

    public delegate void StepTurn(IPlayer currentPlayer); 

    public delegate void LocatePiece(IPiece piece, EMoveResult result);
}

namespace Client
{
}