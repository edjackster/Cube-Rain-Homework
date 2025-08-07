using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _mesh;
    private Color _standertColor;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _standertColor = _mesh.material.color;
    }

    public void RollBack()
    {
        _mesh.material.color = _standertColor;
    }

    public void ChangeColor()
    {
        float minHue = 0;
        float maxHue = 1;
        float saturation = 1;
        float brightness = 1;

        _mesh.material.color = Random.ColorHSV(minHue, maxHue, saturation, saturation, brightness, brightness);
    }
}
