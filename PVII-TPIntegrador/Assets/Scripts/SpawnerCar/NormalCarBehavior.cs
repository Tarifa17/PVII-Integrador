using UnityEngine;

public class NormalCarBehavior : ICarBehavior
{
    private Transform[] pathPoints;
    private float speed = 3f;
    private float t = 0f;
    private int currentIndex = 0;

    private const int samples = 50; // precision del calculo de distancia

    public void SetPath(Transform[] points, float moveSpeed = 3f)
    {
        pathPoints = points;
        speed = moveSpeed;
        currentIndex = 0;
        t = 0f;
    }

    public void Move(GameObject car)
    {
        if (pathPoints == null || pathPoints.Length < 4)
            return;

        // --- Aumentamos t pero seg�n distancia real ---
        t = MoveAlongCurve(t, speed * Time.deltaTime);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = (currentIndex + 1) % pathPoints.Length;
        }

        // Obtener �ndices seguros
        int p0 = (currentIndex - 1 + pathPoints.Length) % pathPoints.Length;
        int p1 = currentIndex;
        int p2 = (currentIndex + 1) % pathPoints.Length;
        int p3 = (currentIndex + 2) % pathPoints.Length;

        // Posicion real
        Vector3 pos = CatmullRom(pathPoints[p0].position,
                                 pathPoints[p1].position,
                                 pathPoints[p2].position,
                                 pathPoints[p3].position,
                                 t);

        car.transform.position = pos;

        // Rotacion (mirar hacia adelante)
        Vector3 futurePos = CatmullRom(pathPoints[p0].position,
                                       pathPoints[p1].position,
                                       pathPoints[p2].position,
                                       pathPoints[p3].position,
                                       Mathf.Clamp01(t + 0.02f));

        Vector2 dir = futurePos - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        car.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // ====== AVANZAR EN LA CURVA POR DISTANCIA REAL ======
    private float MoveAlongCurve(float t, float distance)
    {
        float step = 1f / samples;

        for (int i = 0; i < samples; i++)
        {
            float t1 = t;
            float t2 = Mathf.Clamp01(t + step);

            int p0 = (currentIndex - 1 + pathPoints.Length) % pathPoints.Length;
            int p1 = currentIndex;
            int p2 = (currentIndex + 1) % pathPoints.Length;
            int p3 = (currentIndex + 2) % pathPoints.Length;

            Vector3 a = CatmullRom(pathPoints[p0].position, pathPoints[p1].position,
                                   pathPoints[p2].position, pathPoints[p3].position, t1);

            Vector3 b = CatmullRom(pathPoints[p0].position, pathPoints[p1].position,
                                   pathPoints[p2].position, pathPoints[p3].position, t2);

            float d = Vector3.Distance(a, b);

            if (distance > d)
            {
                distance -= d;
                t = t2;
            }
            else
            {
                float pct = distance / d;
                return Mathf.Lerp(t1, t2, pct);
            }
        }

        return t;
    }

    // ====== CATMULL ROM ======
    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }
}
