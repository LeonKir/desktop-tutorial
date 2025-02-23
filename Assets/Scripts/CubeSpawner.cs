using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube SpawnCube(Vector3 position, Vector3 scale, float splitChance)
    {
        float cubeOffset = 0.5f;

        Vector3 newPosition = position + Random.insideUnitSphere * cubeOffset;
        Cube Cube = Instantiate(_cubePrefab, newPosition, Random.rotation);
        Cube.transform.localScale = scale;
        Cube.SetSplitChance(splitChance);

        return Cube;
    }

    public void RequestSplit(Cube originalCube)
    {
        int minCountCubes = 2;
        int maxCountCubes = 7;
        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
        float bias = 0.5f;
        float decreaseChance = 2.0f;

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = SpawnCube(
                originalCube.transform.position + Random.insideUnitSphere * bias,
                originalCube.transform.localScale * bias,
                originalCube.GetSplitChance() / decreaseChance
            );

            newCube.ChangeColor();
            newCube.GetComponent<ExplosionHandler>().ApplyExplosion(originalCube.transform.position);
        }
    }
}