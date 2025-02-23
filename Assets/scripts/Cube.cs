using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]

public class Cube : MonoBehaviour
{
    private float _splitChance = 1.0f;
    private ColorChanger _colorChanger;
    private CubeSpawner _spawner;

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }

    public void ChangeColor()
    {
        _colorChanger?.ChangeColor();
    }

    public void Split()
    {
        if (Random.value <= _splitChance)
        {
            CreateNewCubes();
        }

        Destroy(gameObject);
    }

    private void CreateNewCubes()
    {
        int minCountCubes = 2;
        int maxCountCubes = 7;
        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
        float bias = 0.5f;
        float decreaseChance = 2.0f;

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = _spawner.SpawnCube(
                transform.position + Random.insideUnitSphere * bias,
                transform.localScale * 0.5f,
                _splitChance / decreaseChance
            );

            newCube.ChangeColor();
            newCube.GetComponent<ExplosionHandler>().ApplyExplosion(transform.position);
        }
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _spawner = FindObjectOfType<CubeSpawner>();
    }
}