using UnityEngine;
using TMPro;

public class LapTracker : MonoBehaviour
{
    public TextMeshProUGUI lapText;
    public int totalLaps = 3;

    private int currentLap = 0;
    private bool lapCycleActive = false;
    private bool passedMidpoint = false;

    private void Start()
    {
        UpdateLapText();
    }

    public void OnStartLineEntered()
    {
        if (passedMidpoint)
        {
            currentLap++;
            passedMidpoint = false;
            UpdateLapText();
            Debug.Log($"Vuelta completada: {currentLap}");

            if (currentLap >= totalLaps)
            {
                Debug.Log("Carrera terminada");
            }
        }

        lapCycleActive = true;
        Debug.Log("Inicio de ciclo de vuelta");
    }

    public void OnMidpointEntered()
    {
        if (lapCycleActive)
        {
            passedMidpoint = true;
            Debug.Log("Midpoint pasado, listo para sumar vuelta");
        }
    }

    private void UpdateLapText()
    {
        if (lapText != null)
            lapText.text = $"Vuelta: {currentLap}/{totalLaps}";
    }
}
