using UnityEngine;

public class DebugVisibility : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (!sr.enabled)
        {
            sr.enabled = true; // lo reactiva para evitar invisibilidad
        }
    }

    private string GetDisabler()
    {
        // Devuelve un stacktrace para saber quién llamó a sr.enabled = false
        return System.Environment.StackTrace;
    }
}
