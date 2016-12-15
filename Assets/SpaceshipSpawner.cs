using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSpawner : MonoBehaviour {

    public GameObject spaceship;

    float lastTime;
    float spawnTime = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastTime > spawnTime)
        {
            SpawnSpaceship();
            lastTime = Time.time;
        }
	}

    void SpawnSpaceship()
    {
        float distanceFromCube = 1000f;
        float posneg = Random.Range(-1, 2);
        posneg = posneg == 0 ? 1 : posneg;


        Vector3 spawnPos = new Vector3(800 * posneg + Random.value * distanceFromCube, 10 * posneg, 800 * posneg + Random.value * distanceFromCube);
        GameObject s = Instantiate(spaceship, spawnPos, Quaternion.identity);
    }
}
