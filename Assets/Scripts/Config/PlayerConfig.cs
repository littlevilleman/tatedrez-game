using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Tatedrez/Config/Player", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        public new string name;
        public Sprite avatar;
        public bool isAI = false;
    }
}