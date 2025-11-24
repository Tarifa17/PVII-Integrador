using UnityEngine;

public class CarPathSetter : MonoBehaviour
{
    public Transform[] pathPoints;
    public GameObject car;

    private NormalCarBehavior normalBehavior;

    private void Start()
    {
        normalBehavior = new NormalCarBehavior();
        normalBehavior.SetPath(pathPoints, 2.4f); // velocidad fija

        // Asignar comportamiento al auto
        car.GetComponent<CarController>().SetBehavior(normalBehavior);
    }
}
