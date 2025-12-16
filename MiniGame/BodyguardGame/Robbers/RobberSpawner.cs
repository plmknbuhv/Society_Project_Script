using System.Collections.Generic;
using UnityEngine;
using Work.Common.Core;
using Work.Min.Code.EventChannel;
using Random = UnityEngine.Random;

namespace Work.SHS.Code.MiniGame.BodyguardGame.Robbers
{
    public class RobberSpawner : MonoBehaviour
    {
        [SerializeField] private float minTime = 1.5f, maxTime = 2f;
        [SerializeField] private Robber robberPrefab;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<Material> robberMaterials;

        private bool _isCanSpawn;
        private float _spawnTime;

        private void Awake()
        {
            GameEventBus.AddListener<StartGameEvent>(HandleStartGame);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<StartGameEvent>(HandleStartGame);
        }

        public void HandleStartGame(StartGameEvent evt)
        {
            _isCanSpawn = true;
            _spawnTime = Time.time;
        }

        private void Update()
        {
            if (_isCanSpawn == false) return;
            if (_spawnTime > Time.time) return;
            
            _spawnTime = Time.time + Random.Range(minTime, maxTime);
            SpawnRobber();
        }

        [ContextMenu("Spawn")]
        public void SpawnRobber()
        {
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Material material = robberMaterials[Random.Range(0, robberMaterials.Count)];
            
            Robber robber = Instantiate(robberPrefab, spawnPoint, Quaternion.Euler(0, -90, 0));
            robber.SetUpEnemy(material);
        }
        
        public void StopSpawn() => _isCanSpawn = false;
    }
}