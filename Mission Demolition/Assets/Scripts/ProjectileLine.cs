using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileLine : MonoBehaviour
{
    static List<ProjectileLine> PROJ_LINES = new();
    private const float DIM_MULT = 0.75f;

    private LineRenderer lineRenderer;
    private bool drawing = true;
    private Projectile projectile;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        projectile = GetComponentInParent<Projectile>();

        ADD_LINE(this);
    }

    void FixedUpdate()
    {
        if (drawing)
		{
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

            if (projectile != null)
			{
                if (!projectile.awake)
				{
                    drawing = false;
                    projectile = null;
				}			
            }
		}
    }

	private void OnDestroy()
	{
        PROJ_LINES.Remove(this);
	}

    static void ADD_LINE(ProjectileLine newLine)
	{
        Color color;

		foreach (var pl in PROJ_LINES)
		{
            color = pl.lineRenderer.startColor;
            color *= DIM_MULT;
            pl.lineRenderer.startColor = pl.lineRenderer.endColor = color;
		}

        PROJ_LINES.Add(newLine);
	}
}
