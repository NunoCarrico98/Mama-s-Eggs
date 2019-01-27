using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YolkController : MonoBehaviour
{
	public float Speed => speed;
	[SerializeField] private GameObject parent;
	[SerializeField] private float speed;
	[SerializeField] private float divisor;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject zombieDestination;

	private float initSpeed;
	private int enemyCounter = 0;

	private void Awake()
	{
		initSpeed = speed;
	}

	// Update is called once per frame
	void Update()
	{
		ChangeShrinkingSpeed();
		Shrink();
	}

	private void Shrink()
	{
		if (speed != initSpeed)
			transform.parent.localScale =
					new Vector3(transform.parent.localScale.x - speed / 1000,
					transform.parent.localScale.y - speed / 1000,
					transform.parent.localScale.z);

		if (transform.parent.localScale.x <= 1.4)
			transform.parent.GetComponent<Blob>().enabled = false;

		if (transform.parent.localScale.x <= 0.3)
		{
			zombieDestination.transform.parent = player.transform;
			Destroy(parent);
		}
	}

	private void ChangeShrinkingSpeed()
	{
		if (enemyCounter != 0) speed = speed + (enemyCounter / divisor);
		else speed = initSpeed;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy") enemyCounter++;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Enemy") enemyCounter--;
	}
}
