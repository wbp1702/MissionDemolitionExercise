using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Inscribed")]
    public float destroyBoundsY = 0f;
    public float destroyVelocity = 0.01f;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rigidbody.isKinematic) return;

        if (rigidbody.velocity.magnitude <= destroyVelocity || transform.position.y <= destroyBoundsY) Destroy(gameObject);
    }
}
