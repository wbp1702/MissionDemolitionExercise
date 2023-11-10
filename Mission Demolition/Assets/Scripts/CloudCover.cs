using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCover : MonoBehaviour
{
    [Header("Inscribed")]
    public Sprite[] cloudSprites;
    public int numClouds = 40;
    public Vector3 minPos = new Vector3(-20, -5, -5);
    public Vector3 maxPos = new Vector3(300, 40, 5);

    public Vector2 scaleRange = new Vector2(1, 4);

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = this.transform;
        GameObject cloudGameObject;
        Transform cloudTransform;
        SpriteRenderer spriteRenderer;
        float scaleMult;

		for (int i = 0; i < numClouds; i++)
		{
            cloudGameObject = new GameObject();
            cloudTransform = cloudGameObject.transform;
            spriteRenderer = cloudGameObject.AddComponent<SpriteRenderer>();

            int spriteNum = Random.Range(0, cloudSprites.Length);
            spriteRenderer.sprite = cloudSprites[spriteNum];

            cloudTransform.position = RandomPos();
            cloudTransform.SetParent(parentTransform, true);

            scaleMult = Random.Range(scaleRange.x, scaleRange.y);
            cloudTransform.localScale = Vector3.one * scaleMult;
		}
    }

    Vector3 RandomPos()
	{
        Vector3 pos = new();
        pos.x = Random.Range(minPos.x, maxPos.x);
        pos.x = Random.Range(minPos.y, maxPos.y);
        pos.x = Random.Range(minPos.z, maxPos.z);
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
