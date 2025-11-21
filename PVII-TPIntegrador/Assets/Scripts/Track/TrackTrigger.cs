using UnityEngine;

public class TrackTrigger : MonoBehaviour
{
    [SerializeField] private TrackGenerator generator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Pedimos al generador una nueva carretera
        generator.SpawnNextSegment(transform.position);
    }
}
