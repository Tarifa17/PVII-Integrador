using UnityEngine;
using System.Collections.Generic;

public class TrackPool : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int poolSize = 10;


    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);

            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetSegment()
    {
        if (pool.Count == 0)
            ExpandPool();


        GameObject obj = pool.Dequeue();
        obj.transform.SetParent(null);  // Para evitar padres persistentes
        return obj;
    }

    private void ExpandPool()
    {
        var obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ReturnSegment(GameObject segment)
    {
        segment.transform.SetParent(transform); // Se guarda dentro del pool visualmente
        segment.SetActive(false);
        pool.Enqueue(segment);

        Debug.Log("DEVOLVIENDO AL POOL: " + segment.name);

    }
}
