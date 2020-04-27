using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrossFyre.Gun
{
    public class GunSpawner : MonoBehaviour
    {
        [SerializeField] private GunController gunPrefab = default;
        [SerializeField] private Transform[] spawnPoints = default;
        [SerializeField] private int maxGuns = 5;
        [SerializeField] private float spawnRate = 2;

        private float timeCounter = 0;
        private float numGuns = 0;
        private List<Transform> availableSpawnPoints = new List<Transform>();
        private Dictionary<GunController, Transform> occupiedSpawnPoints = new Dictionary<GunController, Transform>();

        private void Awake()
        {
            availableSpawnPoints = spawnPoints.ToList();
            GunController.OnDeath += OnGunDied;
        }

        private void Update()
        {
            timeCounter += Time.deltaTime;

            if (!(timeCounter >= spawnRate) || !(numGuns < maxGuns)) return;
            
            SpawnGun();
            timeCounter = 0;
        }

        private void SpawnGun()
        {
            var spawnPoint = availableSpawnPoints.PickRandom();
            if (spawnPoint == null) return;

            var gun = Instantiate(gunPrefab, spawnPoint.position, Quaternion.identity);

            availableSpawnPoints.Remove(spawnPoint);
            occupiedSpawnPoints.Add(gun, spawnPoint);

            numGuns++;
        }

        private void OnGunDied(GunController gun)
        {
            occupiedSpawnPoints.TryGetValue(gun, out var spawnpoint);
            availableSpawnPoints.Add(spawnpoint);
            occupiedSpawnPoints.Remove(gun);

            numGuns--;
        }
    }
}
