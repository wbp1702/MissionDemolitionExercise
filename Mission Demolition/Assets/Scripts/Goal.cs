using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
    public static bool goalMet = false;

	private void OnTriggerEnter(Collider other)
	{
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
		{
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color color = mat.color;
            color.a = 0.75f;
            mat.color = color;
		}
	}
}
