using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] string enemyID;
    public string EnemyID { get => enemyID; }
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField, Range(0f,100f)] float healthDropRate = 50f;
    [SerializeField] string healthBoxID;
    bool initialized;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        initialized = true;
    }

    void OnEnable()
    {
        if(!initialized) return;
        waypointIndex = 0;
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if((Vector2)transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SpawnHealth()
    {
        if (Random.Range(0f, 100f) < healthDropRate)
        {
            GameObject instance = ObjectPoolManager.Instance.GetPooledObject(healthBoxID);
            instance.transform.position = transform.position;
            instance.transform.rotation = Quaternion.identity;
            instance.SetActive(true);
        }
    }
}
