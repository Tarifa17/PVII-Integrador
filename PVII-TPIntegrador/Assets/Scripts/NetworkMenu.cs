using UnityEngine;
using UnityEngine.UI;
using FishNet.Managing;

public class StartHostButton : MonoBehaviour
{
    public Button startHostButton;

    private NetworkManager nm;

    private void Awake()
    {
        nm = FindObjectOfType<NetworkManager>();

        if (startHostButton != null)
            startHostButton.onClick.AddListener(StartHost);
    }

    private void StartHost()
    {
        Debug.Log("Iniciando HOST (Server + Client Local)...");

        // Inicia servidor
        nm.ServerManager.StartConnection();

        // Inicia cliente local
        nm.ClientManager.StartConnection();
    }
}
