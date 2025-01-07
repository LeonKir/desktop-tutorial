using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]
public class Cube : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;
    private float _splitChance = 1.0f;

    private void Awake()
    {
        _cubeSpawner = FindObjectOfType<CubeSpawner>();

        if (_cubeSpawner == null)
        {
            Debug.LogError("CubeSpawner не найден в сцене. Добавьте объект с CubeSpawner!");
        }
    }

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            SplitCube();
        }

        Destroy(gameObject);
    }

    private void SplitCube()
    {
        if (_cubeSpawner == null)
        {
            Debug.LogError("CubeSpawner отсутствует! Убедитесь, что он добавлен в сцене.");
            return;
        }

        int newCubesCount = Random.Range(2, 7);

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = _cubeSpawner.SpawnCube(
                transform.position + Random.insideUnitSphere * 0.5f,
                transform.localScale * 0.5f,
                _splitChance / 2.0f
            );

            ColorChanger colorChanger = newCube.GetComponent<ColorChanger>();
            colorChanger?.ChangeColor();

            ExplosionHandler explosionHandler = newCube.GetComponent<ExplosionHandler>();
            explosionHandler?.ApplyExplosion(transform.position);
        }
    }
}