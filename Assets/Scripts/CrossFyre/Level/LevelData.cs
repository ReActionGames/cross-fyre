using UnityEngine;

namespace CrossFyre.Level
{
    public class LevelData : ScriptableObject
    {
        public int LevelNumber => levelNumber;
        public Wave[] Waves => waves;

        [SerializeField] private int levelNumber = 1;
        [SerializeField] private Wave[] waves;
    }
}