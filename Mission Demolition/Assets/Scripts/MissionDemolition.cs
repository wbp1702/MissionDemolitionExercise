using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd,
}

public class MissionDemolition : MonoBehaviour
{
    private static MissionDemolition Singleton;

    [Header("Inscribed")]
    public Text textLevel;
    public Text textShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        if (Singleton != null)
		{
            Debug.LogError("More than one MissionDemolition Script");
            Destroy(gameObject);
            return;
		}

        Singleton = this;
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
	{
        if (castle != null) Destroy(castle);

        Projectile.DestroyProjectiles();

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCam.Switch_View(FollowCam.eView.both);
	}

    void UpdateGUI()
	{
        textLevel.text = $"Level: {level + 1} of {levelMax}";
        textShots.text = $"Shots Taken: {shotsTaken}";
	}

	private void Update()
	{
        UpdateGUI();

        if (mode == GameMode.playing && Goal.goalMet)
		{
            mode = GameMode.levelEnd;

            FollowCam.Switch_View(FollowCam.eView.both);

            Invoke("NextLevel", 2f);
		}
	}

    void NextLevel()
	{
        level++;
        if (level == levelMax)
		{
            level = 0;
            shotsTaken = 0;
		}

        StartLevel();
	}

    public static void ShotFired()
	{
        Singleton.shotsTaken++;
	}

    public static GameObject GetCastle()
	{
        return Singleton.castle;
	}
}
