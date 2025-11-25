using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlCar : NetworkBehaviour
{
    [SerializeField] private float aceleracion = 10f;
    [SerializeField] private float velocidadMaxima = 8f;
    [SerializeField] private float fuerzaGiro = 3f;
    [SerializeField] private float agarre = 4f;

    private Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        // --- MUY IMPORTANTE ---
        // Si este objeto NO pertenece al cliente local, no debe ejecutar inputs.
        if (!IsOwner)
            return;

        float inputAceleracion = Input.GetAxis("Vertical");
        float inputGiro = Input.GetAxis("Horizontal");

        // --- Acelerar hacia adelante
        rig.AddForce(transform.up * inputAceleracion * aceleracion);

        // Limitar velocidad máxima
        rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, velocidadMaxima);

        // --- Giro basado en velocidad 
        float velocidadActual = rig.linearVelocity.magnitude;
        float giro = inputGiro * fuerzaGiro * (velocidadActual / velocidadMaxima);

        rig.MoveRotation(rig.rotation - giro);

        // ---  (reduce el patinaje)
        Vector2 velocidadLateral = Vector2.Dot(rig.linearVelocity, transform.right) * transform.right;
        Vector2 velocidadAdelante = Vector2.Dot(rig.linearVelocity, transform.up) * transform.up;

        // Reducir velocidad lateral para el giro
        rig.linearVelocity = velocidadAdelante + velocidadLateral * (1f / agarre);
    }
}
