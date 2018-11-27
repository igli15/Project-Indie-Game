using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour {

    private List<EnemySpawner> m_spawners;

    private int m_numberOfActiveEnemies = 0;
    public bool isZoneCleared = false;
    public bool isPlayerInsideZone = false;
	void Start () {
        m_spawners = new List<EnemySpawner>();
        //CallNextWave();

    }
	
	void Update () {
        if (isZoneCleared) return;

        if (m_numberOfActiveEnemies <= 0&&isPlayerInsideZone)
        {
            Debug.Log("ALL ENEMIES ARE DEAD, CALLING A NEW WAVE");
            CallNextWave();
        }
	}

    public void CallNextWave()
    {

        m_numberOfActiveEnemies = 0;
        foreach (EnemySpawner spawner in m_spawners)
        {
            int temp = spawner.SpawnNextWave();
            if (temp == -1)
            {
                isZoneCleared = true;
                Debug.Log("Zone: " + name + " is cleared");
                return;
            }
            m_numberOfActiveEnemies += temp;
        }
        Debug.Log("New Wave: " + m_numberOfActiveEnemies);
    }

    public void AddSpawner(EnemySpawner spawner)
    {
        m_spawners.Add(spawner);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        Debug.Log("Player entered ZONE: " + name);
        isPlayerInsideZone = true;

    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        Debug.Log("Player left ZONE: " + name);
        isPlayerInsideZone = false;
        foreach (EnemySpawner spawner in m_spawners)
        {
            spawner.currentWaveIndex--;
        }
    }

    public int numberOfActiveEnemies
    {
        get { return m_numberOfActiveEnemies; }
        set { m_numberOfActiveEnemies = value; }
    }
}
