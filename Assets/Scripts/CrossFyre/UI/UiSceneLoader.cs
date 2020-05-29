using CrossFyre.Core;
using UnityEngine;
using UnityEngine.Events;

namespace CrossFyre.UI
{
    public class UiSceneLoader : MonoBehaviour
    {
        [EnumAction(typeof(Arena))]
        public void LoadArena(int arena)
        {
            SceneLoader.Instance.LoadArena((Arena) arena);
        }

        [EnumAction(typeof(OtherScene))]
        public void LoadOtherScene(int scene)
        {
            SceneLoader.Instance.LoadOtherScene((OtherScene) scene);
        }
    }
}