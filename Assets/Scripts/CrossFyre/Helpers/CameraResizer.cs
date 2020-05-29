using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre.Helpers
{
    [RequireComponent(typeof(Camera))]
    public class CameraResizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spritesToCover;
        [SerializeField] private float padding = 1f;

        private new Camera camera;

        private Rect targetRect;
        private float ratio;

        private void OnDrawGizmos()
        {
            if (camera == null) return;
            
            var camPosition = camera.transform.position;
            Gizmos.DrawWireCube(camPosition, targetRect.size);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(camPosition, spritesToCover[0].bounds.size);
        }

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void Start()
        {
            ratio = (float)Screen.height / Screen.width;
            Resize();
        }

        [Button]
        private void Resize()
        {
            var viewRect = CalcViewRect(spritesToCover);
            viewRect = AddPadding(viewRect, padding);

            targetRect = viewRect;

            camera.orthographicSize = viewRect.width * ratio * camera.rect.height * 0.5f;
            
            // Adjust for height does not quite work. Fix later
            if (viewRect.height > camera.orthographicSize * 2f)
            {
                camera.orthographicSize = viewRect.height * camera.rect.height * 0.5f;
            }
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

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static Rect AddPadding(Rect rect, float padding)
        {
            return Rect.MinMaxRect(rect.xMin - padding, rect.yMin - padding, rect.xMax + padding, rect.yMax + padding);
        }
        
    }
}
