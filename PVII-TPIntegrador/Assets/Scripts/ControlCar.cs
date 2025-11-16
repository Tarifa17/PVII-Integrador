using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlCar : MonoBehaviour
{
    [SerializeField] private float aceleracion = 100f;
    [SerializeField] private float velocidadMaxima = 10f;
    [SerializeField] private float velocidadGiro = 5f;

    private Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float velocidad = Input.GetAxis("Vertical");
        rig.AddForce(Vector2.up * velocidad * aceleracion * Time.deltaTime);
        rig.velocity = Vector2.ClampMagnitude(rig.velocity, velocidadMaxima);


        float girar = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * girar * velocidadGiro * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -4f, 4f);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
