using UnityEngine;

namespace CrossFyre
{
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;

        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}
