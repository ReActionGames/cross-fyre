using CrossFyre.Level;
using UnityEngine;

namespace CrossFyre.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelData[] levels;

        private bool firstLevelInSession = true;
        private int currentLevel;

        private LevelManager levelManager;

        private void Awake()
        {
            firstLevelInSession = true;
        }

        private void OnEnable()
        {
            GameEvents.LevelSceneReady += StartLevel;
            GameEvents.LevelEnded += StartNextLevelOrEndGame;
        }

        private void OnDisable()
        {
            GameEvents.LevelSceneReady -= StartLevel;
            GameEvents.LevelEnded -= StartNextLevelOrEndGame;
        }

        // private void StartGame()
        // {
        //     firstLevelInSession = false;
        //     currentLevel = 0;
        //     var levelManager = FindObjectOfType<LevelManager>();
        //     levelManager.StartLevel(levels[0]);
        // }

        private void StartLevel()
        {
            if (firstLevelInSession)
            {
                currentLevel = 0;
                firstLevelInSession = false;
            }
            
            levelManager = FindObjectOfType<LevelManager>();
            levelManager.StartLevel(levels[currentLevel]);
        }

        private void StartNextLevelOrEndGame()
        {
            currentLevel++;

            if (currentLevel >= levels.Length)
            {
                Debug.Log("End Game!!");
                GameEvents.TriggerStandardEvent(StandardEvent.GameEnded);
                return;
            }
            
            // SceneLoader.Instance.RestartArena();
            levelManager.StartLevel(levels[currentLevel]);
        }
    }
}
