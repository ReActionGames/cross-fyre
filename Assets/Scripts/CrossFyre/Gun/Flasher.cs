using DG.Tweening;
using UnityEngine;

namespace CrossFyre.Gun
{
    public class Flasher
    {
        public struct FlashSprite
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

        private readonly float duration;

        private readonly FlashSprite[] sprites;
        private readonly Sequence sequence;

        public Flasher(float flashDuration, Color flashedColor, params SpriteRenderer[] renderers)
        {
            this.duration = flashDuration;
            //var renderers = renderers;
            this.sprites = GetFlashSprites(renderers, flashedColor);
            this.sequence = GetSequence(sprites, flashDuration);
        }

        private static FlashSprite[] GetFlashSprites(SpriteRenderer[] renderers, Color color)
        {
            var sprites = new FlashSprite[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
            {
                sprites[i] = new FlashSprite(renderers[i], color);
            }
            return sprites;
        }

        private static Sequence GetSequence(FlashSprite[] sprites, float duration)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(sprites[0].renderer.DOGradientColor(sprites[0].gradient, duration));
            for (int i = 1; i < sprites.Length; i++)
            {
                sequence.Join(sprites[i].renderer.DOGradientColor(sprites[i].gradient, duration));
            }
            sequence.SetLoops(-1);
            sequence.Pause();
            return sequence;
        }
    
        public void StartFlash()
        {
            sequence.Restart();
        }

        public void StopFlash()
        {
            sequence.Pause();
            foreach (var sprite in sprites)
            {
                sprite.renderer.DOColor(sprite.mainColor, duration);
            }
        }
    }
}