using CrossFyre.Player;
using UnityEngine;

namespace CrossFyre.UI
{
    public class UiRestartText : MonoBehaviour
    {
        [SerializeField] private RectTransform text = default;

        private void Start()
        {
            text.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.GameEnded += ActivateText;
        }

        private void OnDisable()
        {
            GameEvents.GameEnded -= ActivateText;
        }

        private void ActivateText()
        {
            text.gameObject.SetActive(true);
        }
    }
}
