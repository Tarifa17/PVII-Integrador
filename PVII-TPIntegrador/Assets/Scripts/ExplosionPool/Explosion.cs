using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 1f;

    private void OnEnable()
    {
        Invoke(nameof(DisableExplosion), duration);
    }

    private void DisableExplosion()
    {
        gameObject.SetActive(false); // vuelve al pool
    }

    private void OnDisable()
    {
        CancelInvoke(); // por si se apaga antes
    }
}
