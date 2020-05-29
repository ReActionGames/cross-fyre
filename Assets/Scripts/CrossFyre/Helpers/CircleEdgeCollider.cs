using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre.Helpers
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class CircleEdgeCollider : MonoBehaviour
    {
        [SerializeField] private int numEdges = 16;
        [SerializeField] private float radius = 5f;

        [Button]
        private void UpdateCollider()
        {
            var edgeCollider = GetComponent<EdgeCollider2D>();
            if (edgeCollider == null)
            {
                Debug.LogError("Cannot update collider. " + gameObject.name + " does not have an edge collider attached!");
                return;
            }
            
            var points = new Vector2[numEdges + 1];

            for (var i = 0; i < numEdges; i++)
            {
                var angle = 2 * Mathf.PI * i / numEdges;
                var x = radius * Mathf.Cos(angle);
                var y = radius * Mathf.Sin(angle);

                points[i] = new Vector2(x, y);
            }

            points[numEdges] = points[0];

            edgeCollider.points = points;
        }
    }
}