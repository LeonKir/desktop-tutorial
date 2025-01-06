using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;

    public Cube SpawnCube(Vector3 position, Vector3 scale, float splitChance)
    {
        float biasVector = 0.5f;
        int biasScale = 2;

        Vector3 newPosition = position + Random.insideUnitSphere * biasVector;
        Cube newCube = Instantiate(cubePrefab, newPosition, Random.rotation);
        newCube.transform.localScale = scale / biasScale;
        newCube.SetSplitChance(splitChance);

        return newCube;
    }
}