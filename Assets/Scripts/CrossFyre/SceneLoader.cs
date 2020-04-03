using System;
using System.Collections;
using System.Collections.Generic;
using CrossFyre.GameSettings;
using Sirenix.OdinInspector;
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
                arenaSceneNames.TryGetValue(arena, out var name);
                return name;
            }
        }

        [TitleGroup("Scene names")] [SerializeField]
        private string persistent = "PersistentScene",
            debug = "DebugScene",
            metaLevel = "MetaLevel";

        [SerializeField] private Dictionary<Arena, string> arenaSceneNames;

        private void OnEnable()
        {
            GameSettingsManager.SettingsChanged += OnSettingsChanged;
        }

        private void OnDisable()
        {
            GameSettingsManager.SettingsChanged -= OnSettingsChanged;
        }

        private void OnSettingsChanged(Settings settings)
        {
            if (settings.debugMode)
            {
                SceneManager.LoadSceneAsync(debug, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync(debug);
            }
        }

        private IEnumerator Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(persistent));
            yield return StartCoroutine(UnloadAllScenesExceptPersistent());
            if (GameSettingsManager.Settings.debugMode)
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