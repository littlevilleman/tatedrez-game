using Config;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class PlayerWidget : MonoBehaviour
    {
        [SerializeField] private Image avatarImage;
        [SerializeField] private GameObject turnHolder;

        public void Setup(PlayerConfig config)
        {
            avatarImage.sprite = config.avatar;
            Highlight(false);
        }

        public void Highlight(bool highlight)
        {
            turnHolder.SetActive(highlight);
        }
    }

}
