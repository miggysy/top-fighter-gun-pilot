using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform path;

    [Header("Wave Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] float spawnRateVariance = 0f;
    [SerializeField] float spawnRateMinimum= 0.2f;

    public int GetEnemyCount() { return enemies.Count; }
    public GameObject GetEnemy(int index) { return enemies[index]; }
    public Transform GetStartingWaypoint() { return path.GetChild(0); }
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in path)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(spawnRate - spawnRateVariance, spawnRate + spawnRateVariance);
        return Mathf.Clamp(spawnTime, spawnRateMinimum, float.MaxValue);
    }

}
