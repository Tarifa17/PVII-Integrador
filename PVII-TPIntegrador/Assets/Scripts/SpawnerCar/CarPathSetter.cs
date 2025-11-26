using UnityEngine;
using FishNet.Object;

public class CarPathSetter : NetworkBehaviour
{
    public Transform[] pathPoints;
 //   public GameObject car;

    private NormalCarBehavior normalBehavior;

    private void Start()
    {
        normalBehavior = new NormalCarBehavior();
        normalBehavior.SetPath(pathPoints, 5.4f); // velocidad fija

        // Asignar comportamiento al auto
       // car.GetComponent<CarController>().SetBehavior(normalBehavior);
    }
}
