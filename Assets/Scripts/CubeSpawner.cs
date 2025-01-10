using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube SpawnCube(Vector3 position, Vector3 scale, float splitChance)
    {
        float bias = 0.5f;

        Vector3 newPosition = position + Random.insideUnitSphere * bias;
        Cube newCube = Instantiate(_cubePrefab, newPosition, Random.rotation);
        newCube.transform.localScale = scale;
        newCube.SetSplitChance(splitChance);

        return newCube;
    }
}