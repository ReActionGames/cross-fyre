using System;
using CrossFyre.Gun;
using UnityEngine;

namespace CrossFyre.Level
{
    [Serializable]
    public struct Node
    {
        public float delay;
        public GunController gun;
        public Vector3 spawnPoint;
    }
}