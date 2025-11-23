using FishNet.Object;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    public Camera cam;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
            cam.enabled = true;
        else
            cam.enabled = false;
    }
}
