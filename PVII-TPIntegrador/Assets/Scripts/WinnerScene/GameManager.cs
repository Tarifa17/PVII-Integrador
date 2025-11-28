using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string winnerName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // evita duplicados
        }
    }
}
