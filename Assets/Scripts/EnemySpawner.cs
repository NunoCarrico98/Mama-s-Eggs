using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxNumberOfEnemies;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minInterval;
    [SerializeField] private float maxInterval;

    private Vector2 centerPos;

    private void Start()
    {
        StartCoroutine(WaitToSpawn());
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
        GameObject newEnemy = Instantiate(zombiePrefab, pos, transform.rotation, transform);

		while (newEnemy.GetComponent<Enemy>().IsOutOfEggWhite())
		{
			Debug.Log("Spawnou fora");
			newEnemy.transform.position = GetSpawnPos();
		}
	}

    private IEnumerator WaitToSpawn()
    {
        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);

            yield return new WaitForSeconds(interval);

            Spawn();
        }
    }
}
