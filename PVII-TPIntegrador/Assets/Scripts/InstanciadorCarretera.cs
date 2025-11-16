using UnityEngine;

public class InstanciadorCarretera : MonoBehaviour
{
    [SerializeField] private ControlCarreteras controlCarreteras;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            controlCarreteras.InstanciarCarretera(transform.position);
        }
        else if (otro.CompareTag("Destructor"))
        {
            Destroy(transform.root.gameObject);

        }
    }
}
