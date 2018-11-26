using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //IMPORTANT
    [System.Serializable]
    public class EnemiesData
    {
        public int numberOfGoombas=0;
        public int numberOfTurrets=0;
    }
    //IMPORTANT
    [HideInInspector]
    public int numWave = 0;

    //IMPORTANT
    public List<EnemySpawner> spawners;

    //IMPORTANT
    [HideInInspector]
    public static EnemyManager instance;

    //IMPORTANT
    private EnemiesData[,] m_enemyWavesData;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        m_enemyWavesData = new EnemiesData[spawners.Count, numWave];

    }
	

	void Update () {
		
	}
}
