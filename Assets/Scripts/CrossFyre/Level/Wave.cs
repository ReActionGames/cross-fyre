using System;
using UnityEngine;

namespace CrossFyre.Level
{
    [Serializable]
    public class Wave
    {
        public Node[] Nodes => nodes;
        public float Delay => delay;
        public int TotalGuns => nodes.Length;

        [SerializeField] private float delay = 1f;
        [SerializeField] private Node[] nodes;
    }
}