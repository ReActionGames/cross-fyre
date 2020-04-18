using System.Collections;
using System.Collections.Generic;
using CrossFyre.GameSettings;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre
{
    public class SceneLoader : SerializedMonoBehaviour
    {
        public enum Arena
        {
            Square,
            Circle
        }

        [SerializeField] private Arena arena = Arena.Circle;

        [ShowInInspector]
        private string ArenaName
        {
            get
            {
                arenaSceneNames.TryGetValue(arena, out var sceneName);
                return sceneName;
            }
        }

        [TitleGroup("Scene names")] [SerializeField]
        private string persistent = "PersistentScene",
            debug = "DebugScene",
            metaLevel = "MetaLevel";

        [SerializeField] private Dictionary<Arena, string> arenaSceneNames;
        [SerializeField] private string sceneFolderPath = "Assets/Scenes";


        private IEnumerator Start()
        {
            yield return LoadSelectedArena(GameSettingsManager.Settings.debugMode);
        }

#if UNITY_EDITOR
        [DisableInPlayMode]
        [Button(ButtonSizes.Large, Name = "Load Selected Arena")]
        private void LoadSelectedArenaEditor()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(persistent));
            while (SceneManager.sceneCount > 1)
            {
                var index = 0;
                var scene = SceneManager.GetSceneAt(index);
                while (scene.name.Equals(persistent))
                {
                    index++;
                    scene = SceneManager.GetSceneAt(index);
                }

                EditorSceneManager.CloseScene(scene, true);
            }
            
            EditorSceneManager.OpenScene(sceneFolderPath + "/" + metaLevel + ".unity", OpenSceneMode.Additive); 

            EditorSceneManager.OpenScene(sceneFolderPath + "/" + ArenaName + ".unity", OpenSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(sceneFolderPath + "/" + ArenaName + ".unity"));
        }
#endif

        private IEnumerator LoadSelectedArena(bool debugMode)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (debugMode)
            {
                yield return SceneManager.LoadSceneAsync(debug, LoadSceneMode.Additive);
            }

            yield return SceneManager.LoadSceneAsync(metaLevel, LoadSceneMode.Additive);

            yield return SceneManager.LoadSceneAsync(ArenaName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(ArenaName));
        }

        public void RestartArena()
        {
            StartCoroutine(RestartArenaAsync());
        }

        public IEnumerator RestartArenaAsync()
        {
            yield return SceneManager.UnloadSceneAsync(ArenaName);
            yield return SceneManager.UnloadSceneAsync(metaLevel);
            yield return SceneManager.LoadSceneAsync(metaLevel, LoadSceneMode.Additive);
            yield return SceneManager.LoadSceneAsync(ArenaName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(ArenaName));
        }

        private IEnumerator UnloadAllScenesExceptPersistent()
        {
            while (SceneManager.sceneCount > 1)
            {
                var index = 0;
                var scene = SceneManager.GetSceneAt(index);
                while (scene.name.Equals(persistent))
                {
                    index++;
                    scene = SceneManager.GetSceneAt(index);
                }

                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}