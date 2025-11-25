using UnityEngine;

public interface ICarFactory
{
    GameObject CreateCar(Vector3 position);
}

public class TimedCarFactory : ICarFactory
{
    private GameObject normalCarPrefab;
    private GameObject crazyCarPrefab;
    private Transform[] pathPoints;
    private float startTime;

    public TimedCarFactory(GameObject normal, GameObject crazy, Transform[] points)
    {
        normalCarPrefab = normal;
        crazyCarPrefab = crazy;
        pathPoints = points;
        startTime = Time.time;
    }

    public GameObject CreateCar(Vector3 position)
    {
        float elapsed = Time.time - startTime;
        GameObject car;

        if (elapsed < 2f)
        {
            car = Object.Instantiate(normalCarPrefab, position, Quaternion.identity);
            var behavior = new NormalCarBehavior();
            behavior.SetPath(pathPoints, 5.4f);
            car.GetComponent<CarController>().SetBehavior(behavior);
        }
        else
        {
            car = Object.Instantiate(crazyCarPrefab, position, Quaternion.identity);
            var behavior = new CrazyCarBehavior();
            behavior.SetPath(pathPoints, 5.4f); // si Crazy también sigue puntos
            car.GetComponent<CarController>().SetBehavior(behavior);
        }

        return car;
    }
}



