using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube SpawnCube(Vector3 position, Vector3 scale, float splitChance)
    {
        float cubeOffset = 0.5f;

        Vector3 newPosition = position + Random.insideUnitSphere * cubeOffset;
        Cube newCube = Instantiate(_cubePrefab, newPosition, Random.rotation);
        newCube.transform.localScale = scale;
        newCube.SetSplitChance(splitChance);

        return newCube;
    }
}