using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre.GameDebug
{
    public class Restarter : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
