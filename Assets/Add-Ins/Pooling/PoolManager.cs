using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReActionGames.Pooling
{
    public class PoolManager : MonoBehaviourSingleton<PoolManager>
    {
        [SerializeField] private List<PoolSettings> defaultPools;

        private Dictionary<PoolSettings, List<Transform>> pools;

        // public static T Take<T>(Vector3 position, Quaternion rotation)
        // {
        // }
    }

    [Serializable]
    public struct PoolSettings
    {
        public Transform prefab;
        
        [Tooltip("The starting size of the pool.")]
        public int startingSize;

        [Tooltip("The name of the pools parent object. Leave empty for no parent.")]
        public string parentName;

        [Tooltip("Should the pool automatically increase its size when all objects are in use?")]
        public bool autoGrow;

        [Tooltip("The amount by which the pool will increase when max size is reached.")]
        public int increaseAmount;


        public static PoolSettings Default =>
            new PoolSettings()
            {
                startingSize = 10,
                parentName = "",
                autoGrow = true,
                increaseAmount = 5
            };
    }

    public interface IPoolable
    {
        // void Init(PoolManager manager);
        void Init();

        void Take();

        void Return();
    }
}