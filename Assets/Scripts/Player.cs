using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxTimeOutsideYolk;
    [SerializeField] private float rollSpeed;
    [SerializeField] private GameObject pointsPrefab;



    public float MaxTimeOutsideYolk => maxTimeOutsideYolk;
    public float DeathTimer { get; private set; }
    public bool IsDying { get; private set; }
    public bool IsReseting { get; private set; }
    public bool Rolling { get; private set; }
    public bool Died { get; private set; }

    private float rollTimer;
    private bool ableToMove = true;
    private bool facingRight = true;
    private bool canRoll = true;

    private Vector2 dir = Vector2.zero;
    private Rigidbody2D rb;
    private Animator animator;
    private ParticleSystem particleSystem;
    private ScoreManager scoreManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSystem.Stop();

    }

    private void Update()
    {
        if (canRoll) CheckRollInput();
        DeathCountdown();
        if (!Rolling) ResetDeathTimer();
    }

    private void FixedUpdate()
    {
        if (ableToMove) MovePlayer();
        if (Rolling) Roll();
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
        if (Input.GetButtonDown("Action") && !Rolling && dir != Vector2.zero)
        {
            DeathTimer += maxTimeOutsideYolk * 0.14f;

            animator.SetTrigger("Roll");
            Rolling = true;
        }
    }

    private void Roll()
    {
        rollTimer += Time.deltaTime;

        if (rollTimer < 0.2)
            rb.MovePosition(rb.position + dir * rollSpeed * Time.deltaTime);
        else
        {
            rollTimer = 0;
            Rolling = false;
        }
    }

    public void CanPlayerRoll(bool enable) => canRoll = enable;

    private void DeathCountdown()
    {
        if (IsDying && DeathTimer < maxTimeOutsideYolk)
        {
            IsReseting = false;
            DeathTimer += Time.deltaTime;
            //if (DeathTimer >= maxTimeOutsideYolk)
            //	StartCoroutine(Die());
        }
    }

    private void ResetDeathTimer()
    {
        if (IsReseting && DeathTimer != 0)
        {
            IsDying = false;
            if (DeathTimer > 0) DeathTimer -= Time.deltaTime * 2;
            else if (DeathTimer < 0) DeathTimer = 0;
        }
    }

    public IEnumerator Die()
    {
        animator.SetTrigger("Die");
        ableToMove = false;
        yield return new WaitForSeconds(0.4f);
        if (transform.childCount > 1) transform.GetChild(1).parent = null;
        transform.GetChild(0).parent = null;
        particleSystem.Stop();
        Died = true;
    }

    private void SetPlayerSpriteDirection(float horizontal)
    {
        // If the input is moving the player right and the player is facing left...
        if (facingRight && horizontal < 0) Flip();
        if (!facingRight && horizontal > 0) Flip();
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
            IsReseting = true;
            particleSystem.Stop();
            ResetDeathTimer();
        }

        if (col.tag == "Enemy" && Rolling)
        {
            StartCoroutine(col.gameObject.GetComponent<Zombie>().Die());
            scoreManager.IncreaseScore();
            DestoyAfterDelay points = 
                Instantiate(pointsPrefab, col.transform.position,
                col.transform.rotation, 
                col.transform)
                .GetComponent<DestoyAfterDelay>();
            if (!col.GetComponent<Zombie>().IsFacingRight)
            {
                Vector3 theScale = col.transform.localScale;
                theScale.x *= -1;
                col.transform.localScale = theScale;
            }
                
            points.TimeToDie = 2;

        }

        if (col.tag == "Trap")
            if (col.GetComponent<TrapBubble>().IsExploding) StartCoroutine(Die());
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "EggWhite")
            StartCoroutine(Die());

        if (col.transform.tag == "YolkIn")
        {
            IsDying = true;
            particleSystem.Play();
        }

    }
}
