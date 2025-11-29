using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        // asegurar que solo exista una instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persistir entre escenas

            // obtener o añadir el audioSource si no existe
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                    Debug.LogWarning("AudioSource no encontrado. Se añadió uno nuevo.");
                }
            }

            // verificar que haya un clip asignado antes de reproducir
            if (audioSource.clip != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else if (audioSource.clip == null)
            {
                Debug.LogWarning("No se ha asignado ningún AudioClip al AudioSource.");
            }
        }
        else
        {
            // destruir duplicados
            Destroy(gameObject);
        }
    }
}