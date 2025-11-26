using UnityEngine;
using System.Collections.Generic;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance { get; private set; }

    public GameObject explosionPrefab;
    public int poolSize = 10;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // opcional: evita duplicados
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetExplosion(Vector3 position)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(explosionPrefab, position, Quaternion.identity);
        pool.Add(newObj);
        return newObj;
    }
}
