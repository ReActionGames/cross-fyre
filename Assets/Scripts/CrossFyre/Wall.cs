using UnityEngine;

namespace CrossFyre
{
    public class Wall : MonoBehaviour
    {
        [SerializeField]
        private WallsManager.Side side;

        public WallsManager.Side Side
        {
            get => side;
            private set => side = value;
        }

        private new BoxCollider2D collider = null;

        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
        }

        public void SetEnabled(bool enabled)
        {
            collider.enabled = enabled;
        }

        public void SetSizeAndPosition(Vector2 topRight, Vector2 bottomLeft)
        {
            collider.size = CalculateSize(topRight, bottomLeft, collider.size, side);
            collider.transform.position = CalculatePosition(topRight, bottomLeft, collider.size, side);
        }

        public static Vector2 CalculateSize(Vector2 topRight, Vector2 bottomLeft, Vector2 colliderSize, WallsManager.Side side)
        {
            float width = topRight.x - bottomLeft.x;
            float height = topRight.y - bottomLeft.y;

            switch (side)
            {
                case WallsManager.Side.North:
                case WallsManager.Side.South:
                    return colliderSize.With(x: width);
                case WallsManager.Side.East:
                case WallsManager.Side.West:
                    return colliderSize.With(y: height);
                default:
                    return colliderSize.With(x: width); // default to north side
            }
        }

        public static Vector2 CalculatePosition(Vector2 topRight, Vector2 bottomLeft, Vector2 colliderSize, WallsManager.Side side)
        {
            float width = topRight.x - bottomLeft.x;
            float height = topRight.y - bottomLeft.y;
            Vector2 origin = Vector2.Lerp(topRight, bottomLeft, 0.5f); // midpoint

            switch (side)
            {
                case WallsManager.Side.North:
                    return origin + Vector2.up * (height + colliderSize.y) / 2;
                case WallsManager.Side.South:
                    return origin + Vector2.down * (height + colliderSize.y) / 2;
                case WallsManager.Side.East:
                    return origin + Vector2.right * (width + colliderSize.x) / 2;
                case WallsManager.Side.West:
                    return origin + Vector2.left * (width + colliderSize.x) / 2;
                default:
                    return origin + Vector2.up * (height + colliderSize.y) / 2; // default to north side
            }
        }
    }
}
