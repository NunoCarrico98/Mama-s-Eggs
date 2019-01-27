using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IEnemy
{
	public GameObject GameObject => gameObject;
	public bool Died { get; private set; }
	[SerializeField] private float health;
	[SerializeField] private float damage;
	[SerializeField] private float movementSpeed;
	[SerializeField] private float slowSpeed;
	[SerializeField] private LayerMask layerMask;

	private YolkController yolkController;
	private Animator anim;
	private Transform target;
	private float spawnTimer;
	private bool ableToMove;
	private Vector2 dir;


	private void Awake()
	{
		anim = GetComponent<Animator>();

		yolkController = FindObjectOfType<YolkController>();

		if (target == null) target = GameObject
		.FindGameObjectWithTag("ZombieDestination").transform;
	}

	private void Start()
	{
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

	public void FlipEnemy()
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
		if (!Died)
		{
			Died = true;
			anim.SetTrigger("Kill");
			ableToMove = false;
			yield return new WaitForSeconds(0.8f);
			Destroy(gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "YolkIn")
		movementSpeed = yolkController.Speed / 20 * (1/ yolkController.Speed);
	}
}
