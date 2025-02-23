using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = Random.ColorHSV();
        }
    }
}