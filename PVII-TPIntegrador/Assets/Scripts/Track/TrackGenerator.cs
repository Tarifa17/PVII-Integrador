using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackPool pool;
    [SerializeField] private Vector2 offset;

    public void SpawnNextSegment(Vector2 previousPos)
    {
        GameObject segment = pool.GetSegment();
        segment.transform.position = previousPos + offset;
        segment.SetActive(true);
    }
}
