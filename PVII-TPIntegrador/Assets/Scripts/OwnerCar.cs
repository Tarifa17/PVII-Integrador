using FishNet.Object;

public class OwnerCar : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        // Asignar propiedad al cliente dueño de este objeto
        GiveOwnership(Owner);
    }
}
