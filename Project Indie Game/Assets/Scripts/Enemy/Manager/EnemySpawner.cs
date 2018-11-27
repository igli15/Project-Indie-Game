using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private EnemyZone m_enemyZone;
    [SerializeField]
    private List<Wave> m_waves;
    
    private EnemyManager m_enemyManager;

    private int m_currentWaveIndex = -1;

    public void Start()
    {
        m_enemyManager = EnemyManager.instance;
        m_enemyZone.AddSpawner(this);
    }

    private void Update()
    {
        
    }

    public void SetWaves(List<Wave> waves)
    {
        m_waves = waves;
    }

    public void AddWave(Wave newWave)
    {
        m_waves.Add(newWave);
    }

    public int SpawnNextWave()
    {
        if (m_currentWaveIndex+1 >= m_waves.Count) return -1;
        m_currentWaveIndex++;
        Debug.Log(name+" spawning next wave "+ m_currentWaveIndex);

        Debug.Log("     Gombas: "+m_waves[m_currentWaveIndex].numberOfGoombas);
        SpawnGoomba(m_waves[m_currentWaveIndex].numberOfGoombas);

        Debug.Log("     Turrets: " + m_waves[m_currentWaveIndex].numberOfTurrets);
        SpawnTurret(m_waves[m_currentWaveIndex].numberOfTurrets);

 
        return m_waves[m_currentWaveIndex].numberOfGoombas + m_waves[m_currentWaveIndex].numberOfTurrets;
    }

    public void SpawnGoomba(int amountOfGoombas = 1)
    {
        for(int i=0;i<amountOfGoombas;i++)
            SpawnEnemy("Goomba");
    }
    public void SpawnTurret(int amountOfTurret= 1)
    {
        for (int i = 0; i < amountOfTurret; i++)
            SpawnEnemy("Turret");
    }

    public void SpawnEnemy(string tag)
    {
        GameObject newEnemy=ObjectPooler.instance.SpawnFromPool(tag, transform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().onEnemyDestroyed += OnMyEnemyDestroyed;
    }

    

    public void OnMyEnemyDestroyed()
    {
        m_enemyZone.numberOfActiveEnemies--;
    }

    public int currentWaveIndex
    {
        get { return m_currentWaveIndex; }
        set { m_currentWaveIndex = value; }
    }
}
