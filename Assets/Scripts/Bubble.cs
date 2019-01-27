using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour, IEnemy
{
	public GameObject GameObject => gameObject;
	[SerializeField] private LayerMask layerMask;

	private void Start()
	{
		StartCoroutine(BubbleLifeExpectancy());
	}

	private IEnumerator BubbleLifeExpectancy()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}

	public bool IsOutOfEggWhite()
	{
		// Cast a ray straight down.
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward,
			10, layerMask);

		// If it hits something...
		if (hit.collider != null)
		{
			if (hit.collider.tag == "EggWhite")
			{
				return false;
			}
		}
		return true;
	}
}
