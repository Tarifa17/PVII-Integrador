using FishNet;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;

[AddComponentMenu("FishNet/Component/PlayerSpawner")]
public class MyPlayerSpawner : MonoBehaviour
{
    [Header("Prefab a spawnear")]
    [SerializeField] private NetworkObject playerPrefab;

    [Header("Posiciones de spawn")]
    [SerializeField] private Transform[] spawnPoints;

    private NetworkManager manager;
    private int nextSpawn = 0;

    private void Awake()
    {
        manager = InstanceFinder.NetworkManager;
        manager.ServerManager.OnRemoteConnectionState += OnClientConnectionChange;
    }

    private void OnClientConnectionChange(NetworkConnection conn, FishNet.Transporting.RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState != FishNet.Transporting.RemoteConnectionState.Started)
            return;

        if (!manager.ServerManager.Started)
            return;

        // Determinar posición
        Vector3 pos;
        Quaternion rot;

        if (spawnPoints.Length > 0)
        {
            pos = spawnPoints[nextSpawn].position;
            rot = spawnPoints[nextSpawn].rotation;
        }
        else
        {
            pos = playerPrefab.transform.position;
            rot = playerPrefab.transform.rotation;
        }

        nextSpawn++;
        if (nextSpawn >= spawnPoints.Length)
            nextSpawn = 0;

        // Spawn desde servidor y asignar ownership al cliente
        NetworkObject obj = Instantiate(playerPrefab, pos, rot);
        manager.ServerManager.Spawn(obj, conn);
    }
}
