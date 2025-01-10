using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ExplosionHandler : MonoBehaviour
{
    public void ApplyExplosion(Vector3 explosionPosition, float force = 10f, float radius = 5f)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 explosionDir = (transform.position - explosionPosition).normalized;
            rb.AddForce(explosionDir * force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody отсутствует! Убедитесь, что компонент добавлен.");
        }
    }
}