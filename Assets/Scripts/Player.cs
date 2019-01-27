using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float maxTimeOutsideYolk;
	[SerializeField] private float rollSpeed;

	private float deathTimer;
	private float rollTimer;
	private bool ableToMove = true;
	private bool isDying;
	private bool isReseting = true;
	private bool facingRight = true;
	private bool rolling;
	private Vector2 dir = Vector2.zero;
	private Rigidbody2D rb;
	private AngleManager angleManager;
	private Animator animator;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		angleManager = FindObjectOfType<AngleManager>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		CheckRollInput();
		DeathCountdown();
		ResetDeathTimer();
	}

	private void FixedUpdate()
	{
		if (ableToMove) MovePlayer();
		if (rolling) Roll();
	}


	private void MovePlayer()
	{
		float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed;
		float vertical = Input.GetAxisRaw("Vertical") * movementSpeed;

		if (horizontal != 0 || vertical != 0) animator.SetBool("Walking", true);
		else animator.SetBool("Walking", false);

		dir = new Vector2(horizontal, vertical);
		rb.MovePosition(rb.position + dir * Time.deltaTime);

		SetPlayerSpriteDirection(horizontal);
	}

	private void CheckRollInput()
	{
		if (Input.GetButtonDown("Action") && !rolling && dir != Vector2.zero)
		{
			animator.SetTrigger("Roll");
			rolling = true;
		}

	}

	private void Roll()
	{
		rollTimer += Time.deltaTime;

		if (rollTimer < 0.2)
		{
			rb.MovePosition(rb.position + dir * rollSpeed * Time.deltaTime);
		}
		else
		{
			rollTimer = 0;
			rolling = false;
		}
	}

	private void DeathCountdown()
	{
		if (isDying && deathTimer < maxTimeOutsideYolk)
		{
			isReseting = false;
			deathTimer += Time.deltaTime;
			if (deathTimer >= maxTimeOutsideYolk)
				StartCoroutine(Die());
		}
	}

	private void ResetDeathTimer()
	{
		if (isReseting && deathTimer != 0)
		{
			isDying = false;
			if (deathTimer > 0) deathTimer -= Time.deltaTime * 2;
			else if (deathTimer < 0) deathTimer = 0;
		}
	}

	private IEnumerator Die()
	{
		animator.SetTrigger("Die");
		ableToMove = false;
		yield return new WaitForSeconds(0.4f);
		gameObject.SetActive(false);
	}

	private void SetPlayerSpriteDirection(float horizontal)
	{
		// If the input is moving the player right and the player is facing left...
		if (facingRight && horizontal < 0) Flip();
		if (!facingRight && horizontal >= 0) Flip();
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Flip the player's body.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "YolkIn")
		{
			isReseting = true;
			ResetDeathTimer();
		}
		if (col.tag == "Enemy" && rolling)
			StartCoroutine(col.gameObject.GetComponent<Zombie>().Die());
		if (col.tag == "Trap")
		{
			if (col.GetComponent<TrapBubble>().IsExploding) StartCoroutine(Die());
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "EggWhite")
			StartCoroutine(Die());

		if (col.transform.tag == "YolkIn")
		{
			isDying = true;
		}
	}
}
