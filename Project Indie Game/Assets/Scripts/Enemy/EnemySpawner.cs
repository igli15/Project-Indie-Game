using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    List<Wave> m_waves;

    private int m_currentWaveIndex = 0;

    public void Start()
    {
        m_waves = new List<Wave>();
    }

    public void SetWaves(List<Wave> waves)
    {
        m_waves = waves;
    }

    public void AddWave(Wave newWave)
    {
        m_waves.Add(newWave);
    }

    public void SpawnNextWave()
    {
        Debug.Log("Spawning next wave");
        Debug.Log("Gombas: "+m_waves[m_currentWaveIndex].numberOfGoombas);
        Debug.Log("Turrets: " + m_waves[m_currentWaveIndex].numberOfTurrets);
        m_currentWaveIndex++;


    }
}
