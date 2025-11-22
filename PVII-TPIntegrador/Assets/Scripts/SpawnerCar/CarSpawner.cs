using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject normalCarPrefab;
    public GameObject crazyCarPrefab;
    private ICarFactory factory;
    private float spawnInterval = 2f;
    private float nextSpawnTime;

    void Start()
    {
        factory = new TimedCarFactory(normalCarPrefab, crazyCarPrefab);
        nextSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Vector3 spawnPos = transform.position;
            factory.CreateCar(spawnPos);
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}
