using System;
using System.Collections;
using UnityEngine;

namespace CrossFyre
{
    public class WallsManager : MonoBehaviour
    {
        public enum Side
        {
            North,
            South,
            East,
            West
        }

        public static event Action<Side> OnWallChanged;

        [SerializeField] private bool rotateActiveWalls = true;
        [SerializeField] private float switchInterval = 3;
        [SerializeField] private new Camera camera;

        private Wall[] walls = null;

        public Side ActiveWall { get; private set; }

        private void Awake()
        {
            walls = GetComponentsInChildren<Wall>();
        }

        private void Start()
        {
            SetWallSizes();

            if (rotateActiveWalls)
            {
                SwitchEnabledWall();
                StartTimer(SwitchEnabledWall, switchInterval);
            }
            else
            {
                EnableAllWalls();
            }
        }

        private void SetWallSizes()
        {
            var topRight = (Vector2) camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
            var bottomLeft = (Vector2) camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));

            foreach (var wall in walls)
            {
                wall.SetSizeAndPosition(topRight, bottomLeft);
            }
        }

        private void StartTimer(Action onComplete, float duration)
        {
            StartCoroutine(PerformActionOnTimer(onComplete, duration));
        }

        private IEnumerator PerformActionOnTimer(Action onComplete, float duration)
        {
            while (true)
            {
                yield return new WaitForSeconds(duration);
                onComplete.Invoke();
            }
        }

        private void SwitchEnabledWall()
        {
            DisableAllWalls();
            var wall = walls.PickRandom();
            wall.SetEnabled(true);
            ActiveWall = wall.Side;
            OnWallChanged?.Invoke(wall.Side);
        }

        private void DisableAllWalls()
        {
            walls.ForEach((x) => x.SetEnabled(false));
        }

        private void EnableAllWalls()
        {
            walls.ForEach(x => x.SetEnabled(true));
        }
    }
}