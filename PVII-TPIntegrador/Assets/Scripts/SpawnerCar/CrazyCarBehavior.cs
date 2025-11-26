using UnityEngine;

public class CrazyCarBehavior : ICarBehavior
{
    private Transform[] pathPoints;

    private float normalSpeed = 3f;
    private float t = 0f;
    private int currentIndex = 0;

    // Boost
    private float boostMultiplier = 1.8f;
    private float boostDuration = 1.2f;
    private float boostCooldown = 3f;

    private float boostEndTime = 0f;
    private float nextBoostTime = 0f;

    private const int samples = 50;

    public void SetPath(Transform[] points, float moveSpeed = 3f)
    {
        pathPoints = points;
        normalSpeed = moveSpeed;

        t = 0f;
        currentIndex = 0;

        nextBoostTime = Time.time + Random.Range(1f, boostCooldown);
    }

    public void Move(GameObject car)
    {
        if (pathPoints == null || pathPoints.Length < 4)
            return;

        // ============================================
        // BOOST, PERO SIN AFECTAR LA ROTACIÓN
        // ============================================
        float currentSpeed = normalSpeed;

        if (Time.time >= nextBoostTime)
        {
            boostEndTime = Time.time + boostDuration;
            nextBoostTime = Time.time + boostCooldown + Random.Range(0.2f, 1f);
        }

        if (Time.time < boostEndTime)
            currentSpeed *= boostMultiplier;

        // ============================================
        // MISMO MOVIMIENTO EXACTO QUE EL NORMAL
        // ============================================
        t = MoveAlongCurve(t, currentSpeed * Time.deltaTime);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = (currentIndex + 1) % pathPoints.Length;
        }

        int p0 = (currentIndex - 1 + pathPoints.Length) % pathPoints.Length;
        int p1 = currentIndex;
        int p2 = (currentIndex + 1) % pathPoints.Length;
        int p3 = (currentIndex + 2) % pathPoints.Length;

        Vector3 pos = CatmullRom(pathPoints[p0].position,
                                 pathPoints[p1].position,
                                 pathPoints[p2].position,
                                 pathPoints[p3].position,
                                 t);

        car.transform.position = pos;

        // ============================================
        // MISMA ROTACIÓN QUE EL NORMAL (SIN SUAVIZADO)
        // ============================================
        Vector3 futurePos = CatmullRom(pathPoints[p0].position,
                                       pathPoints[p1].position,
                                       pathPoints[p2].position,
                                       pathPoints[p3].position,
                                       Mathf.Clamp01(t + 0.02f));

        Vector2 dir = futurePos - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        car.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // ====== MOVE ALONG CURVE ======
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
