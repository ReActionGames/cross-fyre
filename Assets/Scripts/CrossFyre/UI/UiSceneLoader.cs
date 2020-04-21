using UnityEngine;
using UnityEngine.Events;

namespace CrossFyre.UI
{
    public class UiSceneLoader : MonoBehaviour
    {
        [EnumAction(typeof(SceneLoader.Arena))]
        public void LoadArena(int arena)
        {
            SceneLoader.Instance.LoadArena((SceneLoader.Arena) arena);
        }

        [EnumAction(typeof(SceneLoader.OtherScene))]
        public void LoadOtherScene(int scene)
        {
            SceneLoader.Instance.LoadOtherScene((SceneLoader.OtherScene) scene);
        }
    }
}