using UnityEngine;
using FishNet.Object;

public class CarSpawner : NetworkBehaviour
{
    public GameObject normalCarPrefab;
    public GameObject crazyCarPrefab;

    private ICarFactory factory;
    private float spawnInterval = 2f;
    private float nextSpawnTime;

    public override void OnStartServer()
    {
        base.OnStartServer();
        factory = new TimedCarFactory(normalCarPrefab, crazyCarPrefab);
        nextSpawnTime = Time.time;
    }

    private void Update()
    {
        if (!IsServer) return;  // Solo el servidor spawnea

        if (Time.time >= nextSpawnTime)
        {
            Vector3 spawnPos = transform.position;
            GameObject car = factory.CreateCar(spawnPos);

            // MUY IMPORTANTE: spawn en la red
            Spawn(car);

            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}
