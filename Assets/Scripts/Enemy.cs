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

	private Rigidbody2D rb;
	private Vector2 dir;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position,
			target.position, movementSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "YolkOut")
			movementSpeed = slowSpeed;
		else if (col.tag == "YolkIn")
			Destroy(gameObject);
	}
}
