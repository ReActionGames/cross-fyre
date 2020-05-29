#if UNITY_EDITOR
using System.Collections.Generic;
using CrossFyre.Core;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre.Debugging
{
    public class DebugSceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneLoaderData data;

        [PropertyOrder(10), PropertySpace(16)] [SerializeField]
        private Arena selectedArena = Arena.Circle;

        [PropertyOrder(20), PropertySpace(16)] [SerializeField]
        private OtherScene selectedOtherScene = OtherScene.TestMenu;

        private string ArenaName
        {
            get
            {
                data.arenaSceneNames.TryGetValue(selectedArena, out var sceneName);
                return sceneName;
            }
        }

        private string OtherSceneName
        {
            get
            {
                data.otherSceneNames.TryGetValue(selectedOtherScene, out var sceneName);
                return sceneName;
            }
        }

        [DisableInPlayMode, PropertyOrder(11)]
        [Button(ButtonSizes.Large, Name = "Load Selected Arena")]
        private void LoadSelectedArena()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.persistent));
            while (SceneManager.sceneCount > 1)
            {
                var index = 0;
                var scene = SceneManager.GetSceneAt(index);
                while (scene.name.Equals(data.persistent))
                {
                    index++;
                    scene = SceneManager.GetSceneAt(index);
                }

                EditorSceneManager.CloseScene(scene, true);
            }

            EditorSceneManager.OpenScene(data.sceneFolderPath + "/" + data.metaLevel + ".unity",
                OpenSceneMode.Additive);

            EditorSceneManager.OpenScene(data.sceneFolderPath + "/" + ArenaName + ".unity", OpenSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(data.sceneFolderPath + "/" + ArenaName + ".unity"));
        }

        [DisableInPlayMode, PropertyOrder(21)]
        [Button(ButtonSizes.Large, Name = "Load Selected Other Scene")]
        private void LoadSelectedOtherScene()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.persistent));
            while (SceneManager.sceneCount > 1)
            {
                var index = 0;
                var scene = SceneManager.GetSceneAt(index);
                while (scene.name.Equals(data.persistent))
                {
                    index++;
                    scene = SceneManager.GetSceneAt(index);
                }

                EditorSceneManager.CloseScene(scene, true);
            }

            EditorSceneManager.OpenScene(data.sceneFolderPath + "/" + OtherSceneName + ".unity",
                OpenSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(OtherSceneName));
        }
    }
}
#endif
