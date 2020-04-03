using System;
using System.Collections;
using CrossFyre.GameSettings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFyre
{
    public class SceneLoader : MonoBehaviour
    {
        public enum Arena
        {
            Square,
            Circle
        }

        [SerializeField] private Arena arena = Arena.Circle;

        [TitleGroup("Scene names")] [SerializeField]
        private string persistent = "PersistentScene",
            debug = "DebugScene",
            metaLevel = "MetaLevel",
            squareArena = "SquareArena",
            circleArena = "CircleArena";

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
            
            string arenaName;
            switch (arena)
            {
                case Arena.Square:
                    arenaName = squareArena;
                    break;
                case Arena.Circle:
                    arenaName = circleArena;
                    break;
                default:
                    arenaName = squareArena;
                    throw new ArgumentOutOfRangeException();
            }

            yield return SceneManager.LoadSceneAsync(arenaName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(arenaName));
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