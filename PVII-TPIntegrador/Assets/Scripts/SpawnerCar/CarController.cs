using UnityEngine;
using FishNet.Object;

public class CarController : NetworkBehaviour
{
    private ICarBehavior behavior;

    public void SetBehavior(ICarBehavior newBehavior)
    {
        behavior = newBehavior;
    }

    void Update()
    {
        // SOLO EL SERVIDOR mueve los coches
        if (!IsServer) return;

        behavior?.Move(gameObject);
    }
}
