using FishNet;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public NetworkObject playerCarPrefab; // Prefab registrado en NetworkManager → Spawnable Prefabs
    private NetworkManager manager;

    private void Awake()
    {
        manager = InstanceFinder.NetworkManager;
        manager.ServerManager.OnRemoteConnectionState += OnClientConnectionChange;
    }

    private void OnClientConnectionChange(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        // Solo cuando el cliente termina de conectarse
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            // Spawn desde el servidor para replicar a todos los clientes
            if (manager.ServerManager.Started)
            {
                // Determinar posición: primer cliente a la izquierda, segundo a la derecha
                int index = manager.ServerManager.Clients.Count; // Clients es Dictionary<int, NetworkConnection>
                Vector3 spawnPos = (index == 1) ? new Vector3(-2f, 0f, 0f) : new Vector3(2f, 0f, 0f);

                NetworkObject playerObj = Instantiate(playerCarPrefab, spawnPos, Quaternion.identity);

                // Spawn y asignar ownership al cliente conectado
                manager.ServerManager.Spawn(playerObj, conn);
            }
        }
    }
}