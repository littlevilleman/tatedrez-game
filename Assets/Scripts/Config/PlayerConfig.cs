using Core;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Tatedrez/Config/Player", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        public new string name;
        public Sprite avatar;
        public bool isAI = false;

        public IPlayer Build(int id, IPiece[] pieceSet)
        {
            if(isAI)
                return new AIPlayer(id, name, pieceSet);

            return new HumanPlayer(id, name, pieceSet);
        }
    }
}