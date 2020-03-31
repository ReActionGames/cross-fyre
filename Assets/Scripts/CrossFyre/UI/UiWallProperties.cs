using UnityEngine;

namespace CrossFyre.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI Wall Properties")]
    public class UiWallProperties : ScriptableObject
    {
        [SerializeField] private float fadeDuration = 0.2f;
        public float FadeDuration { get => fadeDuration; }

        [SerializeField] private Color activeColor = Color.blue;
        public Color ActiveColor { get => activeColor; }
    
        [SerializeField] private Color inactiveColor = Color.blue;
        public Color InactiveColor { get => inactiveColor; }
    }
}