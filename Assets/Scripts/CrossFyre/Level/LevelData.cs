using UnityEngine;

namespace CrossFyre.Level
{
    public class LevelData : ScriptableObject
    {
        public int LevelNumber => levelNumber;
        public Wave[] Waves => waves;

        public int TotalGuns
        {
            get
            {
                if (totalGuns != null) return totalGuns.Value;

                var sum = 0;
                waves.ForEach(w => sum += w.TotalGuns);
                totalGuns = sum;

                return totalGuns.Value;
            }
        }

        [SerializeField] private int levelNumber = 1;
        [SerializeField] private Wave[] waves;

        private int? totalGuns = null;
    }
}