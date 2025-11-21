using UnityEngine;

public class TrackDestroyer : MonoBehaviour
{
    [SerializeField] private TrackPool pool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLISIONÓ CON: " + other.name);


        if (other.CompareTag("Track"))
            Debug.Log("DEVOLVIENDO AL POOL: " + other.name);
            pool.ReturnSegment(other.gameObject);
    }
}
