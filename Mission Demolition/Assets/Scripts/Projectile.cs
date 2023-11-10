using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Inscribed")]
    public float destroyBoundsY = 0f;
    public float destroyVelocity = 0.01f;

    const int LOOKBACK_COUNT = 10;
    static List<Projectile> Projectiles = new();

    [SerializeField]
    private bool _awake = true;
    public bool awake
	{
        get { return _awake; }
        private set { _awake = value; }
	}

    private Vector3 prevPos;
    private List<float> deltas = new();
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        awake = true;
        prevPos = new Vector3(1000, 1000, 0);
        deltas.Add(1000);

        Projectiles.Add(this);
    }

    void Update()
    {
        if (rigidbody.isKinematic || !awake) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);
        prevPos = transform.position;

        while (deltas.Count > LOOKBACK_COUNT) deltas.RemoveAt(0);

        float maxDelta = deltas.Max();

        if (maxDelta <= Physics.sleepThreshold)
		{
            awake = false;
            rigidbody.Sleep();
		}

        if (rigidbody.velocity.magnitude <= destroyVelocity || transform.position.y <= destroyBoundsY) Destroy(gameObject);
    }

	private void OnDestroy()
	{
        Projectiles.Remove(this);
	}

    public static void DestroyProjectiles()
	{
        foreach (var projectile in Projectiles) Destroy(projectile.gameObject);
	}
}
