using UnityEngine;

public class CrazyCarBehavior : ICarBehavior
{
    public void Move(GameObject car)
    {
        // Zigzag suave
        float zigzag = Mathf.Sin(Time.time * 2f) * 1.5f;
        float forwardSpeed = 5f;

        // Movimiento hacia adelante + zigzag
        car.transform.Translate(new Vector3(zigzag, 0, 1) * Time.deltaTime * forwardSpeed);

        // -------------- ROTACI�N VISUAL DEL AUTO --------------------
        float tiltAngle = zigzag * 15f;
        Quaternion targetRot = Quaternion.Euler(0, 0, tiltAngle);

        car.transform.rotation = Quaternion.Lerp(
            car.transform.rotation,
            targetRot,
            Time.deltaTime * 5f
        );
        // ------------------------------------------------------------

        // L�mite de carril
        Vector3 pos = car.transform.position;
        pos.x = Mathf.Clamp(pos.x, -3f, 3f);
        car.transform.position = pos;

        // ---------- ENDEREZAR EL AUTO EN LOS L�MITES ---------------
        if (pos.x <= -3f + 0.01f || pos.x >= 3f - 0.01f)
        {
            // Rotaci�n instant�nea a 0� en Z
            Quaternion straighten = Quaternion.Euler(0, 0, 0);
            car.transform.rotation = straighten;
        }
        // ------------------------------------------------------------
    }
}
