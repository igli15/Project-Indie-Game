using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<Wave> m_waves;

    private EnemyManager m_enemyManager;

    private int m_currentWaveIndex = 0;

    public void Start()
    {
        m_enemyManager = EnemyManager.instance;
        EnemyManager.OnNextWave += SpawnNextWave;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) SpawnNextWave();
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
        Debug.Log(name+" spawning next wave "+ m_currentWaveIndex);

        Debug.Log("     Gombas: "+m_waves[m_currentWaveIndex].numberOfGoombas);
        SpawnGoomba(m_waves[m_currentWaveIndex].numberOfGoombas);

        Debug.Log("     Turrets: " + m_waves[m_currentWaveIndex].numberOfTurrets);
        SpawnTurret(m_waves[m_currentWaveIndex].numberOfTurrets);

        m_currentWaveIndex++;
    }

    public void SpawnGoomba(int amountOfGoombas = 1)
    {
        for(int i=0;i<amountOfGoombas;i++)
        ObjectPooler.instance.SpawnFromPool("Goomba", transform.position, transform.rotation);
    }
    public void SpawnTurret(int amountOfTurret= 1)
    {
        for (int i = 0; i < amountOfTurret; i++)
            ObjectPooler.instance.SpawnFromPool("Turret", transform.position, transform.rotation);
    }
}
