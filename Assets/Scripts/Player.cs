using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float maxTimeOutsideYolk;
	[SerializeField] private float dashSpeed;

	private float deathTimer;
	private bool isDying;
	private bool facingRight;
	private Vector2 dir = Vector2.zero;
	private Rigidbody2D rb;
	private BoxCollider2D pCol;
	private AngleManager angleManager;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		pCol = GetComponent<BoxCollider2D>();
		angleManager = FindObjectOfType<AngleManager>();
	}

	private void Update()
	{
		MovePlayer();
		SetPlayerSpriteDirection(angleManager.GetAngle());
		DeathCountdown();
	}

	private void MovePlayer()
	{
		float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed;
		float vertical = Input.GetAxisRaw("Vertical") * movementSpeed;

		dir = new Vector2(horizontal, vertical);
		rb.MovePosition(rb.position + dir * Time.deltaTime);
	}

	private void DeathCountdown()
	{
		if (isDying)
		{
			deathTimer += Time.deltaTime;
			if (deathTimer >= maxTimeOutsideYolk)
				gameObject.SetActive(false);
		}
	}

	private void ResetDeathTimer()
	{
		isDying = false;
		deathTimer = 0;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.tag == "YolkOut")
		{
			ResetDeathTimer();
			pCol.isTrigger = true;
		}
	}

	private void OnCollisionExit2D(Collision2D col)
	{
		if (col.transform.tag == "YolkIn")
			isDying = true;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "YolkIn")
			ResetDeathTimer();
		if (col.transform.tag == "Enemy")
			Destroy(col.gameObject);
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "YolkOut")
			pCol.isTrigger = false;
	}

	private void SetPlayerSpriteDirection(float angle)
	{
		// If the input is moving the player right and the player is facing left...
		if (facingRight && (angle > -90 && angle < 90)) Flip();

		if (!facingRight && (angle < -90 || angle > 90)) Flip();
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
}
