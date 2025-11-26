using UnityEngine;
using FishNet.Object;
using TMPro;

public class CarHealth : NetworkBehaviour
{
    [Header("Configuración")]
    public int maxLives = 3;

    private int currentLives;
    private TextMeshProUGUI livesText;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        currentLives = maxLives;
        UpdateLivesUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsOwner) return;

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            currentLives--;
            UpdateLivesUI();
            Debug.Log($"Vida perdida. Vidas restantes: {currentLives}");

            // Destruir el obstáculo en red
            NetworkObject netObj = collision.gameObject.GetComponent<NetworkObject>();
            if (netObj != null)
                netObj.Despawn(); // FishNet: despawn en red
            else
                Destroy(collision.gameObject); // fallback local

            if (currentLives <= 0)
            {
                ExplosionPool.Instance.GetExplosion(transform.position);
                Debug.Log("Auto destruido");
                gameObject.SetActive(false);
                // Podés desactivar el coche, mostrar UI, respawn, etc.
            }
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText == null)
        {
            GameObject textObj = GameObject.Find("vidasText");
            if (textObj != null)
                livesText = textObj.GetComponent<TextMeshProUGUI>();
        }

        if (livesText != null)
            livesText.text = $"Vidas: {currentLives}";
    }
}
