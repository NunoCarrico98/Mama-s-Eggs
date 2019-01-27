using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IEnemy
{
	public GameObject GameObject => gameObject;
	[SerializeField] private Transform target;
	[SerializeField] private float health;
	[SerializeField] private float damage;
	[SerializeField] private float movementSpeed;
	[SerializeField] private float slowSpeed;
	[SerializeField] private LayerMask layerMask;

	private float spawnTimer;
	private bool ableToMove;
	private Vector2 dir;
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void Start()
	{
		if (target == null) target = GameObject.FindGameObjectWithTag("Yolk").transform;
		FlipEnemy();
	}

	private void Update()
	{
		PlaySpawnAnimation();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		if (ableToMove)
			transform.position = Vector2.MoveTowards(transform.position,
				target.position, movementSpeed * Time.deltaTime);
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

	private void PlaySpawnAnimation()
	{
		if (spawnTimer < 1.4f && spawnTimer > -1) spawnTimer += Time.deltaTime;

		if (spawnTimer > 1.4f)
		{
			ableToMove = true;
			spawnTimer = -1;
		}
	}

	private void FlipEnemy()
	{
		if (transform.position.x > target.position.x)
		{
			// Flip the player's body.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	public IEnumerator Die()
	{
		anim.SetTrigger("Kill");
		ableToMove = false;
		yield return new WaitForSeconds(0.8f);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "YolkIn")
			movementSpeed = YolkController.Speed;
	}
}
