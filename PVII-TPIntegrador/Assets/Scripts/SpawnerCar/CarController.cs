using UnityEngine;
public class CarController : MonoBehaviour
{
    private ICarBehavior behavior;

    public void SetBehavior(ICarBehavior newBehavior)
    {
        behavior = newBehavior;
    }

    void Update()
    {
        behavior?.Move(gameObject);
    }
}
