using UnityEngine;

public class ControlCarreteras : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayCarreteras;
    [SerializeField] private Vector2 correccion;

    public void InstanciarCarretera(Vector2 posicionAnterior)
    {
        int numeroAleatorio = Random.Range(0, arrayCarreteras.Length);
        Vector2 nuevaPosicion = posicionAnterior + correccion;
        Instantiate(arrayCarreteras[numeroAleatorio], nuevaPosicion, Quaternion.identity);
    }
}
