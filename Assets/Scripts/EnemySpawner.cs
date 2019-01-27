using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private int maxNumberOfEnemies;
	[SerializeField] private GameObject zombiePrefab;
	[SerializeField] private float minRadius;
	[SerializeField] private float maxRadius;
	[SerializeField] private float minInitialInterval;
	[SerializeField] private float maxInitialInterval;

    private float minInterval;
    private float maxInterval;
    private bool isZombieSpawner;
    private bool isTrapSpawner;
    private LevelManager lvlManager;
    private GameManager gm;
	private Vector2 centerPos;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        gm = FindObjectOfType<GameManager>();
        if (name.Contains("Zombie")) isZombieSpawner = true;
        if (name.Contains("Trap")) isTrapSpawner = true;
    }

    private void Start()
	{
		StartCoroutine(WaitToSpawn());
	}

    private void Update()
    {
        if(isZombieSpawner) IncrementZombieDifficulty();
        if (isTrapSpawner) IncrementZombieDifficulty();
    }

    private void IncrementZombieDifficulty()
    {
        minInterval = minInitialInterval +
            (float)ProceduralGeneration.Logistic(
            lvlManager.PlayTime * lvlManager.MinSpawnIntervalZombieValues[3],
            lvlManager.MinSpawnIntervalZombieValues[0],
            lvlManager.MinSpawnIntervalZombieValues[1],
            lvlManager.MinSpawnIntervalZombieValues[2]);

        maxInterval = maxInitialInterval +
            (float)ProceduralGeneration.Logistic(
            lvlManager.PlayTime * lvlManager.MinSpawnIntervalZombieValues[3],
            lvlManager.MaxSpawnIntervalZombieValues[0],
            lvlManager.MaxSpawnIntervalZombieValues[1],
            lvlManager.MaxSpawnIntervalZombieValues[2]);
    }


    private void IncrementTrapDifficulty()
    {
        minInterval = minInitialInterval +
            (float)ProceduralGeneration.Logistic(
            lvlManager.PlayTime * lvlManager.MinSpawnIntervalTrapValues[3],
            lvlManager.MinSpawnIntervalTrapValues[0],
            lvlManager.MinSpawnIntervalTrapValues[1],
            lvlManager.MinSpawnIntervalTrapValues[2]);

        maxInterval = maxInitialInterval +
            (float)ProceduralGeneration.Logistic(
            lvlManager.PlayTime * lvlManager.MinSpawnIntervalTrapValues[3],
            lvlManager.MinSpawnIntervalTrapValues[0],
            lvlManager.MinSpawnIntervalTrapValues[1],
            lvlManager.MinSpawnIntervalTrapValues[2]);
    }


    public Vector2 GetSpawnPos()
	{
		float ang = Random.value * 360;
		float radius = Random.Range(minRadius, maxRadius);
		Vector2 pos;
		pos.x = (centerPos.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad));
		pos.y = (centerPos.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad));
		return pos;
	}

	private void Spawn()
	{
		centerPos = transform.position;

		Vector2 pos = GetSpawnPos();

		//Quaternion rot = Quaternion.LookRotation(Vector3.forward, center - pos);
		IEnemy newEnemy = Instantiate(
			zombiePrefab, pos, transform.rotation, transform).GetComponent<IEnemy>();

		if (newEnemy is Zombie)
		{
			while (newEnemy.IsOutOfEggWhite())
			{
				newEnemy.GameObject.transform.position = GetSpawnPos();
			}
		}
		if (newEnemy is TrapBubble)
		{
			while (newEnemy.IsOutOfEggWhite())
			{
				newEnemy.GameObject.transform.position = GetSpawnPos();
			}
		}
		if (newEnemy is Bubble)
		{
			while (newEnemy.IsOutOfEggWhite())
			{
				newEnemy.GameObject.transform.position = GetSpawnPos();
			}
		}

        if(newEnemy is TrapBubble)
            newEnemy.GameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
	}

	private IEnumerator WaitToSpawn()
	{
		while (true)
		{
			float interval = Random.Range(minInterval, maxInterval);

			yield return new WaitForSeconds(interval);

            if (!gm.IsGamePaused)
            {
                Spawn();

            }
		}
	}
}
