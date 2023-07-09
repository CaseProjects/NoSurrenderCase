using Events;
using UnityEngine;
using Zenject;

public class FoodFactory : MonoBehaviour
{
    #region INJECT

    private CollectableFood.Factory _foodPool;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(CollectableFood.Factory foodPool, SignalBus signalBus)
    {
        _foodPool = foodPool;
        _signalBus = signalBus;
    }

    #endregion

    private MeshCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _signalBus.Subscribe<SignalFoodCollected>(_ => CreateFood());
        CreateStartingFoods();
    }

    private void CreateStartingFoods()
    {
        for (var i = 0; i < 10; i++)
            CreateFood();
    }

    private void CreateFood()
    {
        var randomPoint = GetRandomPointInBounds(_collider.bounds);
        var food = _foodPool.Create();
        food.transform.position = randomPoint;
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        var randomPointAroundBounds = GetRandomPointAroundBounds(bounds);
        var pointInBounds = _collider.ClosestPoint(randomPointAroundBounds);
        pointInBounds.y = 2;
        return pointInBounds;
    }

    private Vector3 GetRandomPointAroundBounds(Bounds bounds)
    {
        var randomPointAroundBounds = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0,
            Random.Range(bounds.min.z, bounds.max.z)
        );
        return randomPointAroundBounds;
    }
}