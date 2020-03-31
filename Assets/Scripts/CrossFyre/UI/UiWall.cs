using DG.Tweening;
using ReActionGames.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiWall : MonoBehaviour
    {
        [SerializeField] private WallsManager.Side side = WallsManager.Side.North;
        [SerializeField] private Image image = null;
        [InlineEditor]
        [SerializeField] private UiWallProperties properties = null;

        private void Awake()
        {
            if (image == null)
                image = GetComponent<Image>();
            image.color = image.color.With(a: 0);
        }

        private void OnEnable()
        {
            WallsManager.OnWallChanged += HandleWallChanged;
        }

        private void OnDisable()
        {
            WallsManager.OnWallChanged -= HandleWallChanged;
        }

        private void HandleWallChanged(WallsManager.Side activeSide)
        {
            SetImageActive(activeSide == side);
        }

        private void SetImageActive(bool active)
        {
            Color color = active == true ? properties.ActiveColor : properties.InactiveColor;

            image.DOColor(color, properties.FadeDuration);
        }
    }
}