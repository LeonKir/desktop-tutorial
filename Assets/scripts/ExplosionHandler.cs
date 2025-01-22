using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ExplosionHandler : MonoBehaviour
{
    public void ApplyExplosion(Vector3 explosionPosition, float force = 10f)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            Vector3 explosionDir = (transform.position - explosionPosition).normalized;
            rigidbody.AddForce(explosionDir * force, ForceMode.Impulse);
        }
    }
}