using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner cubeSpawner;
    private float _splitChance = 1.0f;

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
        int minCountCubes = 2;
        int maxCountCubes = 7;
        float divisionSplitChance = 2.0f;

        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
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

    [System.Serializable]

    public class CubeSpawner
    {
        [SerializeField] private Cube cubePrefab;

        public Cube SpawnCube(Vector3 position, Vector3 scale, float splitChance)
        {
            float biasVector = 0.5f;
            int biasScale = 2;

            Vector3 newPosition = position + Random.insideUnitSphere * biasVector;
            Cube newCube = Object.Instantiate(cubePrefab, newPosition, Random.rotation);
            newCube.transform.localScale = scale / biasScale;
            newCube.SetSplitChance(splitChance);

            return newCube;
        }
    }
}
