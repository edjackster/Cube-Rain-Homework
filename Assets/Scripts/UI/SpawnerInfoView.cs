using TMPro;
using UnityEngine;

public class SpawnerInfoView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TMP_Text _spawnedCountText;
    [SerializeField] private TMP_Text _createdCountText;
    [SerializeField] private TMP_Text _activeCountText;

    private void OnEnable()
    {
        _spawnedCountText.text = _spawner.SpawnedObjectsCount.ToString();
        _createdCountText.text = _spawner.CreatedObjectsCount.ToString();
        _activeCountText.text = _spawner.ActiveObjectsCount.ToString();
        
        _spawner.CreatedObjectsCountChanged += OnCreatedCountChane;
        _spawner.SpawnedObjectsCountChanged += OnSpawnedCountChane;
        _spawner.ActiveObjectsCountChanged += OnActiveCountChane;
    }

    private void OnDisable()
    {
        _spawner.CreatedObjectsCountChanged -= OnCreatedCountChane;
        _spawner.SpawnedObjectsCountChanged -= OnSpawnedCountChane;
        _spawner.ActiveObjectsCountChanged -= OnActiveCountChane;
    }

    private void OnActiveCountChane(int count)
    {
        _activeCountText.text = count.ToString();
    }

    private void OnCreatedCountChane(int count)
    {
        _createdCountText.text = count.ToString();
    }

    private void OnSpawnedCountChane(int count)
    {
        _spawnedCountText.text = count.ToString();
    }
}
