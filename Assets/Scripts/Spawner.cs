using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField, Min(1)] private int _poolCapacity = 10;
    [SerializeField, Min(1)] private int _poolMaxSize = 100;
    [SerializeField] protected T Prefab;
    
    protected ObjectPool<T> Pool;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: GetObject,
            actionOnRelease: ReleaseObject,
            actionOnDestroy: DestroyObject,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    protected abstract void GetObject(T cube);

    protected abstract void ReleaseObject(T cube);

    protected abstract void DestroyObject(T cube);

    protected abstract T CreateObject();
}
