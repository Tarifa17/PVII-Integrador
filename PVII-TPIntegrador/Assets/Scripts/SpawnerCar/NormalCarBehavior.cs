using UnityEngine;
public class NormalCarBehavior : ICarBehavior
{
    public void Move(GameObject car)
    {
        car.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
    }
}
