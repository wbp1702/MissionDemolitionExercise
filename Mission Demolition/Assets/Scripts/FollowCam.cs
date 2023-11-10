using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private static FollowCam Singleton;
    public static GameObject PointOfIntrest;

    public enum eView
	{
        none,
        slingshot,
        castle,
        both,
	}

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public GameObject viewBothGameObject;

    [Header("Dynamic")]
    public float cameraZPosition;
    public eView nextView = eView.slingshot;
    
    void Awake()
    {
        Singleton = this;
        cameraZPosition = transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 destination = Vector3.zero;

        if (PointOfIntrest != null)
		{
            Rigidbody rigidbody = PointOfIntrest.GetComponent<Rigidbody>();
            if (rigidbody != null && rigidbody.IsSleeping()) PointOfIntrest = null;
            else destination = PointOfIntrest.transform.position;
		}


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination.z = cameraZPosition;

        transform.position = Vector3.Lerp(transform.position, destination, easing);
        Camera.main.orthographicSize = destination.y + 10;
    }

    public void SwitchView(eView newView)
	{
        if (newView == eView.none) newView = nextView;

        switch (newView)
		{
            case eView.slingshot:
                PointOfIntrest = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                PointOfIntrest = MissionDemolition.GetCastle();
                nextView = eView.both;
                break;
            case eView.both:
                PointOfIntrest = viewBothGameObject;
                nextView = eView.slingshot;
                break;
        }
    }

    public void SwitchView()
	{
        SwitchView(eView.none);
	}

    public static void Switch_View(eView newView)
	{
        Singleton.SwitchView(newView);
	}
}
