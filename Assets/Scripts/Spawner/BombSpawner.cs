using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _spawnPos;
    
    public void Spawn(Vector3 spawnPos)
    {
        _spawnPos = spawnPos;
        Pool.Get();
    }
    
    protected override void GetObject(Bomb bomb)
    {
        bomb.transform.position = _spawnPos;
        bomb.transform.rotation = Quaternion.identity;
        bomb.RollBack();
        
        bomb.gameObject.SetActive(true);
        bomb.StartTimer();
    }

    protected override void ReleaseObject(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }

    protected override void DestroyObject(Bomb bomb)
    {
        bomb.Exploded -= OnBombExploded;
        Destroy(bomb.gameObject);
    }

    protected override Bomb CreateObject()
    {
        var bomb = Instantiate(Prefab, _spawnPos, Quaternion.identity);
        bomb.Exploded += OnBombExploded;
        bomb.StartTimer();
        return bomb;
    }

    private void OnBombExploded(Bomb cube)
    {
        Pool.Release(cube);
    }
}
