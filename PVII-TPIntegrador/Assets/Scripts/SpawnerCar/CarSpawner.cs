using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet;
using FishNet.Transporting;

public class CarSpawner : NetworkBehaviour
{
    public NetworkObject normalCarPrefab;
    public NetworkObject crazyCarPrefab;
    public Transform[] pathPoints;

    private float spawnInterval = 2f;
    private float nextSpawnTime;
    private int spawnCount = 0;
    private int maxSpawns = 3;

    private bool clientConnected = false;

    public override void OnStartServer()
    {
        base.OnStartServer();

        InstanceFinder.ServerManager.OnRemoteConnectionState += HandleClientState;
    }

    private void OnDestroy()
    {
        if (InstanceFinder.ServerManager != null)
            InstanceFinder.ServerManager.OnRemoteConnectionState -= HandleClientState;
    }

    private void HandleClientState(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            Debug.Log("Cliente conectado → Inicia el spawner.");
            clientConnected = true;
            nextSpawnTime = Time.time + 2f;
        }
    }

    private void Update()
    {
        if (!IsServer) return;
        if (!clientConnected) return;

        if (Time.time >= nextSpawnTime && spawnCount < maxSpawns)
        {
            SpawnCar();
            spawnCount++;
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnCar()
    {
        Vector3 spawnPos = transform.position;
        Quaternion spawnRot = Quaternion.identity;

        // 50% normal, 50% crazy
        bool spawnCrazy = Random.value > 0.5f;

        NetworkObject prefab = spawnCrazy ? crazyCarPrefab : normalCarPrefab;

        // Instanciar en servidor
        NetworkObject carNO = Instantiate(prefab, spawnPos, spawnRot);

        // Configurar comportamiento
        CarController controller = carNO.GetComponent<CarController>();

        if (controller == null)
        {
            Debug.LogError("El prefab no tiene CarController.");
            return;
        }

        if (spawnCrazy)
        {
            CrazyCarBehavior crazy = new CrazyCarBehavior();
            crazy.SetPath(pathPoints);
            controller.SetBehavior(crazy);
        }
        else
        {
            NormalCarBehavior normal = new NormalCarBehavior();
            normal.SetPath(pathPoints);
            controller.SetBehavior(normal);
        }

        // Spawn real en red
        Spawn(carNO);
    }
}
