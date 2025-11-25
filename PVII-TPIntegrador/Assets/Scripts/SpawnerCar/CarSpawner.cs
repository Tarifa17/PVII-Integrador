using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Managing.Scened;

public class CarSpawner : NetworkBehaviour
{
    public GameObject normalCarPrefab;
    public GameObject crazyCarPrefab;
    public Transform[] pathPoints;

    private ICarFactory factory;
    private float spawnInterval = 2f;
    private float nextSpawnTime;
    private int spawnCount = 0;
    private int maxSpawns = 3;

    private bool clientReady = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        factory = new TimedCarFactory(normalCarPrefab, crazyCarPrefab, pathPoints);

        // Esperar a que el cliente cargue la escena
        SceneManager.OnClientLoadedStartScenes += HandleClientReady;
    }

    private void HandleClientReady(NetworkConnection conn, bool loadedSuccessfully)
    {
        if (loadedSuccessfully)
        {
            Debug.Log("Cliente listo: " + conn.ClientId);
            clientReady = true;
            nextSpawnTime = Time.time + 3f; // iniciar spawns 1 segundo después
        }
        else
        {
            Debug.LogWarning("Cliente " + conn.ClientId + " no cargó la escena correctamente.");
        }
    }

    private void Update()
    {
        if (!IsServer || !clientReady) return;

        if (Time.time >= nextSpawnTime && spawnCount < maxSpawns)
        {
            Vector3 spawnPos = transform.position;
            GameObject car = factory.CreateCar(spawnPos);

            Spawn(car);

            spawnCount++;
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}
