using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(.01f, 10)] private float _spawnDelay = 1f;
    [SerializeField, Min(1)] private int _poolCapacity = 10;
    [SerializeField, Min(1)] private int _poolMaxSize = 100;
    [SerializeField, Range(1,10)] private int _spawnsCount = 4;
    [SerializeField] private Cube _prefab;
    
    private Color _standartColor = Color.gray;
    private ObjectPool<Cube> _cubesPool;
    private bool _canSpawn = false;

    private void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(
            createFunc: CreateCube,
            actionOnGet: GetCube,
            actionOnRelease: ReleaseCube,
            actionOnDestroy: DestroyCube,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        var startTime = 0;

        if(_prefab.TryGetComponent<Renderer>(out var renderer) == true)
            _standartColor = renderer.sharedMaterial.color;
        
        _canSpawn = true;
        StartCoroutine(SpawnCube());
    }

    private void OnDrawGizmos()
    {
        var size = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }

    private IEnumerator SpawnCube()
    {
        int i;

        while (_canSpawn)
        {
            for(i = 0; i<_spawnsCount; i++)
                _cubesPool.Get();
            
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void GetCube(Cube cube)
    {
        var position = GetRandomPosition();

        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.RollBack();

        cube.gameObject.SetActive(true);
    }

    private void ReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void DestroyCube(Cube cube)
    {
        cube.Died -= OnCubeDie;
        Destroy(cube.gameObject);
    }

    private Cube CreateCube()
    {
        var postion = GetRandomPosition();
        var cube = Instantiate(_prefab, postion, Quaternion.identity);
        cube.Died += OnCubeDie;

        return cube;
    }

    private void OnCubeDie(Cube cube)
    {
        _cubesPool.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        var scaleDevider = 2;

        var sizeX = transform.localScale.x / scaleDevider;
        var sizeZ = transform.localScale.z / scaleDevider;

        float x = Random.Range(-sizeX, sizeX) + transform.position.x;
        float z = Random.Range(-sizeZ, sizeZ) + transform.position.z;
        return new Vector3(x, transform.position.y, z);
    }
}
