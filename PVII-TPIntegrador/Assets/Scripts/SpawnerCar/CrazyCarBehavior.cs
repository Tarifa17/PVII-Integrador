using UnityEngine;

public class CrazyCarBehavior : ICarBehavior
{
    private Transform[] pathPoints;
    private float baseSpeed = 3f;
    private float t = 0f;
    private int currentIndex = 0;
    private float startTime;
    private float accelerationDuration = 2f;

    private const int samples = 50;

    private float zigzagAmplitude = 0.15f;
    private float zigzagFrequency = 6f;

    public void SetPath(Transform[] points, float moveSpeed = 3f)
    {
        pathPoints = points;
        baseSpeed = moveSpeed;
        currentIndex = 0;
        t = 0f;
        startTime = Time.time;
    }

    public void Move(GameObject car)
    {
        if (pathPoints == null || pathPoints.Length < 4)
            return;

        float elapsed = Time.time - startTime;
        float dynamicSpeed = Mathf.SmoothStep(1.5f, baseSpeed, elapsed / accelerationDuration);

        t = MoveAlongCurve(t, dynamicSpeed * Time.deltaTime);

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

        Vector3 futurePos = CatmullRom(pathPoints[p0].position,
                                       pathPoints[p1].position,
                                       pathPoints[p2].position,
                                       pathPoints[p3].position,
                                       Mathf.Clamp01(t + 0.02f));

        // Zigzag vertical solo entre punto2→punto3 y punto5→punto6
        if ((p1 == 1 && p2 == 2) || (p1 == 4 && p2 == 5))
        {
            float offsetY = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
            float fade = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(0.1f, 0.9f, t));
            pos.y += offsetY * fade;
        }

        car.transform.position = pos;

        Vector2 dir = futurePos - pos;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = car.transform.rotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 8f);
        car.transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
    }

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
                float pct = (d > Mathf.Epsilon) ? distance / d : 0f;
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
