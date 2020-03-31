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
            FindObjectOfType<PlayerController>().GetComponent<HealthComponent>().OnDeath.AddListener(ActivateText);
        }

        private void OnDisable()
        {
            FindObjectOfType<PlayerController>()?.GetComponent<HealthComponent>().OnDeath.RemoveListener(ActivateText);
        }

        private void ActivateText()
        {
            text.gameObject.SetActive(true);
        }
    }
}
