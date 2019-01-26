using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float health;
	[SerializeField] private float damage;
	[SerializeField] private float movementSpeed;
	[SerializeField] private float slowSpeed;
	[SerializeField] private LayerMask layerMask;

	private Vector2 dir;

	private void Start()
	{
		if (target == null) target = GameObject.FindGameObjectWithTag("Yolk").transform;
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position,
			target.position, movementSpeed * Time.deltaTime);
	}

	public bool IsOutOfEggWhite()
	{
		// Cast a ray straight down.
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up,
			10, layerMask);

		// If it hits something...
		if (hit.collider != null)
		{
			Debug.Log("Detetou um collider");
			if (hit.collider.tag == "EggWhite")
			{
				Debug.Log("Detetou a eggwhite");
				return false;
			}
		}
		Debug.Log("Saiu em false");
		return true;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "YolkOut")
			movementSpeed = slowSpeed;
		else if (col.tag == "YolkIn")
			Destroy(gameObject);

	}
}
