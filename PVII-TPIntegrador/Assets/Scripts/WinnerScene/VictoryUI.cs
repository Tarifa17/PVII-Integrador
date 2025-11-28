using UnityEngine;
using TMPro;

public class VictoryUI : MonoBehaviour
{
    public TMP_Text winnerText;

    void Start()
    {
        // Accedemos al GameManager singleton
        if (GameManager.Instance != null)
        {
            winnerText.text = "¡Ganador: " + GameManager.Instance.winnerName + "!";
        }
        else
        {
            winnerText.text = "¡Ganador: ???";
        }
    }
}
