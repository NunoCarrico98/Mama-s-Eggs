using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Transform center;
	[SerializeField] private float smallRadius;
	[SerializeField] private float bigRadius;

	private void SpawnEnemy()
	{
		
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawSphere(center.position, smallRadius);
		Gizmos.color = new Color(0, 1, 0, 0.5f);
		Gizmos.DrawSphere(center.position, bigRadius);
	}
}
