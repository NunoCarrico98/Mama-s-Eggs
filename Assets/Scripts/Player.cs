using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float maxTimeOutsideYolk;

	private float deathTimer;
	private bool isDying;
	private Vector2 dir = Vector2.zero;
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();	
	}

	private void Update()
	{
		MovePlayer();
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

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy")
			Destroy(col.gameObject);
		if (col.tag == "YolkOut")
			ResetDeathTimer();
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "YolkOut")
			isDying = true;
	}
}
