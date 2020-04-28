using System;
using System.Collections;
using UnityEngine;

namespace CrossFyre.Gun
{
    public class MonoFlash : MonoBehaviour
    {
        private struct FlashSprite
        {
            public readonly SpriteRenderer renderer;
            public readonly Color mainColor;
            public readonly Gradient gradient;

            public FlashSprite(SpriteRenderer renderer, Color flashedColor)
            {
                this.renderer = renderer;
                this.mainColor = renderer.color;
                this.gradient = new Gradient()
                {
                    alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1, 0) },
                    colorKeys = new GradientColorKey[] {
                        new GradientColorKey(mainColor, 0),
                        new GradientColorKey(flashedColor, 0.5f),
                        new GradientColorKey(mainColor, 1)
                    },
                    mode = GradientMode.Blend
                };
            }
        }

        [SerializeField] private float flashDuration = 0.2f;
        [SerializeField] private Color flashedColor = Color.white;
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        
        private FlashSprite[] sprites;
        private bool flash;
        private float startTime;
        private float elapsedTime;

        private void Awake()
        {
            sprites = GetFlashSprites(spriteRenderers, flashedColor);
        }


        private static FlashSprite[] GetFlashSprites(SpriteRenderer[] renderers, Color color)
        {
            var sprites = new FlashSprite[renderers.Length];
            for (var i = 0; i < renderers.Length; i++)
            {
                sprites[i] = new FlashSprite(renderers[i], color);
            }
            return sprites;
        }

        public void StartFlash()
        {
            startTime = Time.time;
            elapsedTime = 0f;
            
            flash = true;
        }

        public void StopFlash()
        {
            flash = false;

            foreach (var sprite in sprites)
            {
                sprite.renderer.color = sprite.mainColor;
            }
        }

        private void Update()
        {
            if (!flash) return;

            elapsedTime += Time.deltaTime;
            var percent = elapsedTime / flashDuration;
            percent -= Mathf.Floor(percent); // Only use digits after decimal point

            foreach (var sprite in sprites)
            {
                var color = sprite.gradient.Evaluate(percent);
                sprite.renderer.color = color;
            }
        }
    }
}
