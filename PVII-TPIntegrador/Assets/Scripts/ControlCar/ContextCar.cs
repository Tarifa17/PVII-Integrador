using Assets.Scripts.ControlCar;
using UnityEngine;

/// <summary>
/// Clase que representa el automóvil.
/// Es el "Contexto" que recibe comandos y ejecuta las estrategias.
/// </summary>
public class ContextCar : MonoBehaviour
{
    //Referencia al Rigidbody2D del coche
    public Rigidbody2D Rig { get; private set; }

    // Strategy actual de movimiento
    public IMovementStrategy MovementStrategy { get; set; }

    private float currentAccelInput;
    private float currentTurnInput;
    private void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
        Rig.gravityScale = 0;
    }

    /// <summary>
    /// Setea el input de aceleración (comando).
    /// </summary>
    public void ApplyAcceleration(float accel)
    {
        currentAccelInput = accel;
        ApplyMovement();
    }

    /// <summary>
    /// Setea el input de giro (comando).
    /// </summary>
    public void ApplyTurn(float turn)
    {
        currentTurnInput = turn;
        ApplyMovement();
    }

    /// <summary>
    /// Aplica el movimiento combinando los últimos inputs.
    ///</summary>
    private void ApplyMovement()
    {
        MovementStrategy?.Move(this, currentAccelInput, currentTurnInput);
    }
}
