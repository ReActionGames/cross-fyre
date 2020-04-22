using System.Collections;
using CrossFyre.GameSettings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre
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

    public class SceneLoader : SerializedMonoBehaviourSingleton<SceneLoader>
    {
        [InlineEditor(Expanded = true)] [SerializeField]
        private SceneLoaderData data;


        private Arena currentArena;

        private void Start()
        {
            if (!Application.isEditor)
            {
                LoadOtherScene(OtherScene.TestMenu);
            }
        }

        public void LoadArena(Arena arena)
        {
            var debugMode = GameSettingsManager.Settings.debugMode;
            StartCoroutine(LoadArenaAsync(arena, debugMode));
        }

        private IEnumerator LoadArenaAsync(Arena arena, bool debugMode = false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (debugMode)
            {
                yield return SceneManager.LoadSceneAsync(data.debug, LoadSceneMode.Additive);
            }

            yield return SceneManager.LoadSceneAsync(data.metaLevel, LoadSceneMode.Additive);

            data.arenaSceneNames.TryGetValue(arena, out var arenaName);
            yield return SceneManager.LoadSceneAsync(arenaName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(arenaName));
            currentArena = arena;
        }

        public void LoadOtherScene(OtherScene scene)
        {
            var debugMode = GameSettingsManager.Settings.debugMode;
            StartCoroutine(LoadOtherSceneAsync(scene, debugMode));
        }

        private IEnumerator LoadOtherSceneAsync(OtherScene scene, bool debugMode = false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (debugMode)
            {
                yield return SceneManager.LoadSceneAsync(data.debug, LoadSceneMode.Additive);
            }

            data.otherSceneNames.TryGetValue(scene, out var sceneName);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        public void RestartArena()
        {
            LoadArena(currentArena);
        }

        private IEnumerator UnloadAllScenesExceptPersistent()
        {
            while (SceneManager.sceneCount > 1)
            {
                var index = 0;
                var scene = SceneManager.GetSceneAt(index);
                while (scene.name.Equals(data.persistent))
                {
                    index++;
                    scene = SceneManager.GetSceneAt(index);
                }

                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}