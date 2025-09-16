using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField, Range(.01f, 10)] private float _spawnDelay = 1f;
    [SerializeField, Range(1,10)] private int _spawnsCount = 4;
    [SerializeField] private BombSpawner _bombSpawner;
    
    private bool _canSpawn = false;

    protected override void Awake()
    {
        base.Awake();
        
        _canSpawn = true;
        StartCoroutine(SpawnCube());
    }

    private void OnDrawGizmos()
    {
        var size = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }

    protected override void GetObject(Cube cube)
    {
        var position = GetRandomPosition();

        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.RollBack();

        cube.gameObject.SetActive(true);
    }

    protected override void ReleaseObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    protected override void DestroyObject(Cube cube)
    {
        cube.Died -= OnCubeDie;
        Destroy(cube.gameObject);
    }

    protected override Cube CreateObject()
    {
        var postion = GetRandomPosition();
        var cube = Instantiate(Prefab, postion, Quaternion.identity);
        cube.Died += OnCubeDie;
        
        return cube;
    }

    private IEnumerator SpawnCube()
    {
        int i;

        while (_canSpawn)
        {
            for(i = 0; i<_spawnsCount; i++)
                Pool.Get();
            
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void OnCubeDie(Cube cube)
    {
        _bombSpawner.Spawn(cube.transform.position);
        Pool.Release(cube);
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
