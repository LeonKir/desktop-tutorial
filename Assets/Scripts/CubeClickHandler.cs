using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private float _explosionForce = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                cube.ChangeColor();
                cube.Split();

                ApplyExplosionForce(hit.point);
            }
        }
    }

    private void ApplyExplosionForce(Vector3 explosionPosition)
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (var rigidbodi in rigidbodies)
        {
            if (rigidbodi.TryGetComponent(out ExplosionHandler explosionHandler))
            {
                explosionHandler.ApplyExplosion(explosionPosition, _explosionForce);
            }
        }
    }
}