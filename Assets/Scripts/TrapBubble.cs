using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBubble : MonoBehaviour, IEnemy
{
	public GameObject GameObject => gameObject;
	public bool IsExploding { get; private set; }

	[SerializeField] private LayerMask layerMask;

	private void Start()
	{
		StartCoroutine(TrapBubbleLifeExpectancy());
	}

	private IEnumerator TrapBubbleLifeExpectancy()
	{
		yield return new WaitForSeconds(0.5f);
		IsExploding = true;

		GetComponent<Collider2D>().enabled = true;

		yield return new WaitForSeconds(0.2f);
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
