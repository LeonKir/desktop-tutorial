using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;

    private ColorChanger _colorChanger;
    private ExplosionHandler _explosionHandler;
    private float _splitChance = 1.0f;

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            SplitCube();
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }

    private void ChangeColor()
    {
        _colorChanger?.ChangeColor();
    }

    private void ApplyExplosion(Vector3 explosionPosition)
    {
        _explosionHandler?.ApplyExplosion(explosionPosition);
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _explosionHandler = GetComponent<ExplosionHandler>();
    }

    private void SplitCube()
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
                transform.localScale * bias,
                _splitChance / decreaseChance
            );

            newCube.ChangeColor();
            newCube.ApplyExplosion(transform.position);
        }
    }
}