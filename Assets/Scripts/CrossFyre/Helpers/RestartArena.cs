using CrossFyre.Core;
using UnityEngine;

namespace CrossFyre.Helpers
{
    public class RestartArena : MonoBehaviour
    {
        // Called by UI buttons
        public void Restart()
        {
            FindObjectOfType<SceneLoader>().RestartArena();
        }
    }
}
