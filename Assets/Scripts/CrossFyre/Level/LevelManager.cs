using System;
using System.Collections.Generic;
using CrossFyre.Gun;
using UnityEngine;

namespace CrossFyre.Level
{
    [Serializable]
    public class Node
    {
        [SerializeField] private GunController gun;
        [SerializeField] private Vector3 spawnPoint;
    }

    [Serializable]
    public class Wave
    {
        [SerializeField] private Node[] nodes;
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;
    }
}