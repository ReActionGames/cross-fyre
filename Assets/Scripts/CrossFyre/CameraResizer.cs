using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrossFyre
{
    [RequireComponent(typeof(Camera))]
    public class CameraResizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spritesToCover;
        [SerializeField] private float padding = 1f;
        // [SerializeField] private float accuracy = 0.1f;

        private new Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void Start()
        {
            var viewRect = CalcViewRect(spritesToCover);
            viewRect = AddPadding(viewRect, padding);
        }

        private static Rect CalcViewRect(IEnumerable<SpriteRenderer> renderers)
        {
            return CalcViewRectFromBounds(renderers.Select(sprite => sprite.bounds));
        }

        public static Rect CalcViewRectFromBounds(IEnumerable<Bounds> bounds)
        {
            var max = Vector2.zero;
            var min = Vector2.zero;

            foreach (var b in bounds)
            {
                var sprintMin = b.min;
                var spriteMax = b.max;

                max = new Vector2(Mathf.Max(max.x, spriteMax.x), Mathf.Max(max.y, spriteMax.y));
                min = new Vector2(Mathf.Min(min.x, sprintMin.x), Mathf.Min(min.y, sprintMin.y));
            }

            return new Rect(min.With(y: -min.y), max.With(y: -max.y));
        }

        public static Rect AddPadding(Rect rect, float padding)
        {
            return new Rect(rect.xMin - padding, rect.yMax + padding, rect.xMax + padding, rect.yMin - padding);
        }
        
        private static bool PointInCameraView(Vector3 point)
        {
            return point.z > 0 && point.x > 0 && point.x < 1 && point.y > 0 &&
                   point.y < 1;
        }
    }
}
