using Assets.Scripts.ControlCar;
using UnityEngine;

public class ContextCar : MonoBehaviour
{
    public Rigidbody2D Rig { get; private set; }

    // Strategy actual de movimiento
    public IMovementStrategy MovementStrategy { get; set; }

    private void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
        Rig.gravityScale = 0;
    }

    // Este método lo llaman los comandos
    public void ApplyMovement(float accelInput, float turnInput)
    {
        MovementStrategy?.Move(this, accelInput, turnInput);
    }
}
