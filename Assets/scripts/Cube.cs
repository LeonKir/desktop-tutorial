using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]

public class Cube : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;
    private ColorChanger _colorChanger;
    private ExplosionHandler _explosionHandler;
    private float _splitChance = 1.0f;

    private void Awake()
    {
        _cubeSpawner = FindObjectOfType<CubeSpawner>();
        _colorChanger = GetComponent<ColorChanger>();
        _explosionHandler = GetComponent<ExplosionHandler>();

        if (_cubeSpawner == null)
        {
            Debug.LogError("CubeSpawner �� ������ � �����. �������� ������ � CubeSpawner!");
        }

        if (_colorChanger == null)
        {
            Debug.LogError("ColorChanger ����������� �� ����!");
        }

        if (_explosionHandler == null)
        {
            Debug.LogError("ExplosionHandler ����������� �� ����!");
        }
    }

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }

    public void ChangeColor()
    {
        _colorChanger?.ChangeColor();
    }

    public void ApplyExplosion(Vector3 explosionPosition)
    {
        _explosionHandler?.ApplyExplosion(explosionPosition);
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
            Debug.LogError("CubeSpawner �����������! ���������, ��� �� �������� � �����.");
            return;
        }

        int minCountCubes = 2;
        int maxCountCubes = 7;
        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
        float bias = 0.5f;
        float decrease�hance = 2.0f;

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = _cubeSpawner.SpawnCube(
                transform.position + Random.insideUnitSphere * bias,
                transform.localScale * bias,
                _splitChance / decrease�hance
            );

            newCube.ChangeColor();
            newCube.ApplyExplosion(transform.position);
        }
    }
}