using CrossFyre.Interfaces;
using CrossFyre.Player;
using UnityEngine;

namespace CrossFyre.UI
{
    public class UiRestartText : MonoBehaviour, IResetable
    {
        [SerializeField] private RectTransform text = default;

        private void Start()
        {
            ResetObject();
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

        public void ResetObject()
        {
            text.gameObject.SetActive(false);
        }
    }
}
