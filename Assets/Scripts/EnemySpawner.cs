using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(currentWave.GetEnemy(i).GetComponent<EnemyAI>().EnemyID);
                    if(enemy != null)
                    {
                        enemy.transform.position = currentWave.GetStartingWaypoint().position;
                        enemy.transform.rotation = Quaternion.Euler(0,0,180);
                        enemy.SetActive(true);
                    }
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (true);
    }
}
