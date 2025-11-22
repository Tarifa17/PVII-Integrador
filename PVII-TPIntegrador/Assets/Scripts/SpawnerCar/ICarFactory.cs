using UnityEngine;

public interface ICarFactory
{
    GameObject CreateCar(Vector3 position);
}

public class TimedCarFactory : ICarFactory
{
    public GameObject normalCarPrefab;
    public GameObject crazyCarPrefab;
    private float startTime;

    public TimedCarFactory(GameObject normal, GameObject crazy)
    {
        normalCarPrefab = normal;
        crazyCarPrefab = crazy;
        startTime = Time.time;
    }

    public GameObject CreateCar(Vector3 position)
    {
        float elapsed = Time.time - startTime;
        GameObject car;

        if (elapsed < 10f)
        {
            car = Object.Instantiate(normalCarPrefab, position, Quaternion.identity);
            car.GetComponent<CarController>().SetBehavior(new NormalCarBehavior());
        }
        else
        {
            car = Object.Instantiate(crazyCarPrefab, position, Quaternion.identity);
            car.GetComponent<CarController>().SetBehavior(new CrazyCarBehavior());
        }

        return car;
    }
}
