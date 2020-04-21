using System.Collections;
using System.Collections.Generic;
using CrossFyre.GameSettings;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre
{
    public class SceneLoader : SerializedMonoBehaviourSingleton<SceneLoader>
    {
        public enum Arena
        {
            Square,
            Circle
        }

        public enum OtherScene
        {
            TestMenu
        }

        [SerializeField] private Arena selectedArena = Arena.Circle;
        // [SerializeField] private OtherScene selectedOtherScene = OtherScene.TestMenu;

        [ShowInInspector]
        private string ArenaName
        {
            get
            {
                arenaSceneNames.TryGetValue(selectedArena, out var sceneName);
                return sceneName;
            }
        }

        [TitleGroup("Scene names")] [SerializeField]
        private string persistent = "PersistentScene",
            debug = "DebugScene",
            metaLevel = "MetaLevel";

        [SerializeField] private Dictionary<Arena, string> arenaSceneNames;
        [SerializeField] private Dictionary<OtherScene, string> otherSceneNames;
        [SerializeField] private string sceneFolderPath = "Assets/Scenes";

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

        public void LoadArena(Arena arena)
        {
            var debugMode = GameSettingsManager.Settings.debugMode;
            StartCoroutine(LoadArenaAsync(arena, debugMode));
        }

        private IEnumerator LoadArenaAsync(Arena arena, bool debugMode = false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (debugMode)
            {
                yield return SceneManager.LoadSceneAsync(debug, LoadSceneMode.Additive);
            }

            yield return SceneManager.LoadSceneAsync(metaLevel, LoadSceneMode.Additive);

            arenaSceneNames.TryGetValue(arena, out var arenaName);
            yield return SceneManager.LoadSceneAsync(arenaName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(arenaName));
        }

        public void LoadOtherScene(OtherScene scene)
        {
            var debugMode = GameSettingsManager.Settings.debugMode;
            StartCoroutine(LoadOtherSceneAsync(scene, debugMode));
        }

        private IEnumerator LoadOtherSceneAsync(OtherScene scene, bool debugMode = false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (debugMode)
            {
                yield return SceneManager.LoadSceneAsync(debug, LoadSceneMode.Additive);
            }
            
            otherSceneNames.TryGetValue(scene, out var sceneName);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
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