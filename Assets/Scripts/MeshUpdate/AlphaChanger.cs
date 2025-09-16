using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AlphaChanger : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    
    private Color _currentColor;
    private Color _standardColor;
    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _standardColor = _mesh.material.color;
        _currentColor = _mesh.material.color;
    }

    private void OnEnable()
    {
        _timer.TimeChanged += ChangeColor;
    }

    private void OnDisable()
    {
        _timer.TimeChanged -= ChangeColor;
    }

    public void RollBack()
    {
        _mesh.material.color = _standardColor;
        _currentColor = _mesh.material.color;
    }

    private void ChangeColor(float alpha)
    {
        _currentColor.a = alpha;
        _mesh.material.color = _currentColor;
    }
}
