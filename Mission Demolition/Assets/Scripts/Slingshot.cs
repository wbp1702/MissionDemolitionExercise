using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float launchSpeedMultiplier = 10f;
    public GameObject projLinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPosition;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPosition = launchPointTransform.position;
    }

    private void Update()
    {
        if (!aimingMode || projectile == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float maxMagnitude = GetComponent<SphereCollider>().radius;
        Vector3 mouseDelta = Vector3.ClampMagnitude(mousePosition - launchPosition, maxMagnitude);
        
        Vector3 projectilePosition = launchPosition + mouseDelta;
        projectile.transform.position = projectilePosition;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.velocity = -mouseDelta * launchSpeedMultiplier;
            FollowCam.Switch_View(FollowCam.eView.slingshot);
            FollowCam.PointOfIntrest = projectile;
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null;

            MissionDemolition.ShotFired();
        }
    }

    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = launchPosition;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
