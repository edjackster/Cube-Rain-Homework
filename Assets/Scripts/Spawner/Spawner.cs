using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField, Min(1)] private int _poolCapacity = 10;
    [SerializeField, Min(1)] private int _poolMaxSize = 100;
    [SerializeField] protected T Prefab;
    
    protected ObjectPool<T> Pool;
    private int _spawnedObjectsCount = 0;
    private int _createdObjectsCount = 0;
    private int _activeObjectsCount = 0;
    
    public int SpawnedObjectsCount => _spawnedObjectsCount;
    public int CreatedObjectsCount => _createdObjectsCount;
    public int ActiveObjectsCount => _activeObjectsCount;
    
    public event Action<int> SpawnedObjectsCountChanged;
    public event Action<int> CreatedObjectsCountChanged;
    public event Action<int> ActiveObjectsCountChanged;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: ActionCreate,
            actionOnGet: ActionGet,
            actionOnRelease: ActionRelease,
            actionOnDestroy: DestroyObject,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void ActionGet(T obj)
    {
        _activeObjectsCount = Pool.CountActive;
        ActiveObjectsCountChanged?.Invoke(_activeObjectsCount);
        SpawnedObjectsCountChanged?.Invoke(++_spawnedObjectsCount);
        GetObject(obj);
    }

    private void ActionRelease(T obj)
    {
        _activeObjectsCount = Pool.CountActive;
        ActiveObjectsCountChanged?.Invoke(Pool.CountActive);
        ActiveObjectsCountChanged?.Invoke(_activeObjectsCount);
        ReleaseObject(obj);
    }

    protected virtual T ActionCreate()
    {
        CreatedObjectsCountChanged?.Invoke(++_createdObjectsCount);
        return CreateObject();
    }

    protected abstract void ReleaseObject(T obj);
    protected abstract void DestroyObject(T obj);

    protected abstract void GetObject(T obj);

    protected abstract T CreateObject();

}
