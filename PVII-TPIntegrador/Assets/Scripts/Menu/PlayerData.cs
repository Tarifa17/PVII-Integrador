using FishNet.Object;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    public string playerName;

    public override void OnStartClient()
    {
        base.OnStartClient();

        // solo el dueño envía su nombre al servidor
        if (IsOwner)
        {
            string savedName = PlayerPrefs.GetString("PlayerName", "Jugador");
            ServerSetName(savedName);
        }
    }

    [ServerRpc]
    private void ServerSetName(string name)
    {
        playerName = name;

        // sincronizar con todos los clientes
        ObserversSetName(name);
    }

    [ObserversRpc]
    private void ObserversSetName(string name)
    {
        playerName = name;
    }
}
