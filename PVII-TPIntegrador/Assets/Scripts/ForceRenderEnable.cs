using FishNet.Object;
using UnityEngine;

public class ForceRenderEnable : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        // Fuerza el renderizado en todos los clientes
        foreach (var r in GetComponentsInChildren<Renderer>(true))
        {
            r.enabled = true;
        }

        Debug.Log("Renderers activados en cliente");
    }
}
