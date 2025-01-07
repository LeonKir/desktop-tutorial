using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    public void ChangeColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }
        else
        {
            Debug.LogError("Renderer отсутствует! Убедитесь, что компонент добавлен.");
        }
    }
}
