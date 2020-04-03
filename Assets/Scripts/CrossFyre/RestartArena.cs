using UnityEngine;

namespace CrossFyre
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
