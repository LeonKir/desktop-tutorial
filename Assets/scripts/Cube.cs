using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]

public class Cube : MonoBehaviour
{
    private CubeSpawner cubeSpawner;
    private float _splitChance = 1.0f;

    private void Awake()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();

        if (cubeSpawner == null)
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

        else
        {
            Destroy(gameObject);
        }
    }

    private void SplitCube()
    {
        if (cubeSpawner == null)
        {
            Debug.LogError("CubeSpawner отсутствует! Убедитесь, что он добавлен в сцену.");

            return;
        }

        int minCountCubes = 2;
        int maxCountCubes = 7;
        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);

        float divisionSplitChance = 2.0f;

        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        Debug.Log($"Split Chance: {_splitChance}");

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = cubeSpawner.SpawnCube(originalPosition, originalScale, _splitChance / divisionSplitChance);

            ColorChanger colorChanger = newCube.GetComponent<ColorChanger>();
            colorChanger?.ChangeColor();

            ExplosionHandler explosionHandler = newCube.GetComponent<ExplosionHandler>();
            explosionHandler?.ApplyExplosion(originalPosition);
        }

        Destroy(gameObject);
    }
}